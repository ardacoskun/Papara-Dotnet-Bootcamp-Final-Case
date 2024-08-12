using AutoMapper;
using Para.Business.Services.Interfaces;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.DTOs.Payment;
using Para.Schema.Entities.DTOs.Order;
using Para.Schema.Entities.Models;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace Para.Business.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICreditCardPaymentService _creditCardPaymentService;
    private readonly IOrderService _orderService;

    public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, ICreditCardPaymentService creditCardPaymentService, IOrderService orderService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _creditCardPaymentService = creditCardPaymentService;
        _orderService = orderService;
    }

    public async Task<PaymentDTO> MakePaymentAsync(int userId, string cardNumber, string cardHolderName, string expirationDate, string cvv, string? couponCode)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("User not found.");

        var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
        if (cart == null || !cart.CartItems.Any())
            throw new InvalidOperationException("Cart is empty.");

        decimal amount = cart.CartItems.Sum(item => item.Product.Price * item.Quantity);
        decimal discountedAmount = amount;
        decimal couponAmount = 0;
        int pointsToUse = 0;
        string couponName = "";

        Coupon coupon = null;
        if (!string.IsNullOrEmpty(couponCode))
        {
            coupon = await _unitOfWork.Coupons.GetByCodeAsync(couponCode);
            if (coupon == null || !coupon.IsActive || coupon.IsUsed)
            {
                throw new InvalidOperationException("Invalid or inactive coupon code.");
            }

            couponAmount = coupon.DiscountAmount;
            discountedAmount -= couponAmount;
            coupon.IsUsed = true;
            _unitOfWork.Coupons.Update(coupon);
            couponName = coupon.Code;
        }

        if (user.PointsBalance > 0)
        {
            pointsToUse = Math.Min(user.PointsBalance, (int)discountedAmount);
            user.PointsBalance -= pointsToUse;
            discountedAmount -= pointsToUse;
        }

        if (discountedAmount > 0 && user.WalletBalance >= discountedAmount)
        {
            user.WalletBalance -= discountedAmount;
        }
        else if (discountedAmount > 0 && user.WalletBalance < discountedAmount)
        {
            decimal remainingAmount = discountedAmount - user.WalletBalance;
            bool paymentSuccess = await _creditCardPaymentService.ProcessPayment(cardNumber, cardHolderName, expirationDate, cvv, remainingAmount);

            if (!paymentSuccess)
                throw new InvalidOperationException("Credit card payment failed.");

            user.WalletBalance = 0;
        }

        decimal totalPointsEarned = cart.CartItems.Sum(item =>
        {
            decimal itemAmount = item.Product.Price * item.Quantity;
            decimal points = (itemAmount * item.Product.PointsPercentage) / 100;
            return Math.Min(points, item.Product.MaxPoints * item.Quantity); 
        });

        user.PointsBalance += (int)totalPointsEarned;

        var payment = new Payment
        {
            UserId = userId,
            Amount = amount,
            DiscountedAmount = discountedAmount,
            CouponAmount = couponAmount,
            PointsUsed = pointsToUse,
            CouponName = couponName, 
            PaymentDate = DateTime.UtcNow,
            PaymentDetails = cart.CartItems.Select(ci => new PaymentDetail
            {
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                ProductPrice = ci.Product.Price,
                Quantity = ci.Quantity
            }).ToList()
        };

        await _unitOfWork.Payments.AddAsync(payment);

        foreach (var item in cart.CartItems)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
            if (product != null)
            {
                product.StockQuantity -= item.Quantity;
                _unitOfWork.Products.Update(product);
            }
        }

        await _unitOfWork.CommitAsync();

        var createOrderDTO = new CreateOrderDTO
        {
            UserId = userId,
            TotalAmount = discountedAmount,
            CouponCode = couponCode,
            CouponAmount = couponAmount,
            PointsUsed = pointsToUse,
            OrderDetails = cart.CartItems.Select(ci => new OrderDetailDTO
            {
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                ProductPrice = ci.Product.Price,
                Quantity = ci.Quantity
            }).ToList()
        };

        await _orderService.CreateOrderAsync(createOrderDTO);

        await _unitOfWork.Carts.ClearCartAsync(userId);

        return _mapper.Map<PaymentDTO>(payment);
    }


    public async Task<IEnumerable<PaymentDTO>> GetPaymentHistoryByUserIdAsync(int userId)
    {
        var payments = await _unitOfWork.Payments.GetAllAsync(p => p.UserId == userId, include: p => p.Include(pd => pd.PaymentDetails));

        if (payments == null || !payments.Any())
        {
            throw new InvalidOperationException("No payment history found.");
        }

        return _mapper.Map<IEnumerable<PaymentDTO>>(payments);
    }
}
