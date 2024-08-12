using AutoMapper;
using Para.Business.Services.Interfaces;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.DTOs.Order;
using Para.Schema.Entities.Models;

namespace Para.Business.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO)
    {
        var order = _mapper.Map<Order>(createOrderDTO);
        order.OrderDate = DateTime.UtcNow;

        decimal amount = createOrderDTO.OrderDetails.Sum(od => od.ProductPrice * od.Quantity);
        decimal discountedAmount = amount - createOrderDTO.CouponAmount - createOrderDTO.PointsUsed;

        order.TotalAmount = discountedAmount; 

        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.CommitAsync();

        var orderDto = _mapper.Map<OrderDTO>(order);
        orderDto.Amount = amount;
        orderDto.DiscountedAmount = discountedAmount;

        return orderDto;
    }

    public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
    {
        var order = await _unitOfWork.Orders.GetOrderWithDetailsAsync(orderId);
        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId)
    {
        var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<OrderDTO>>(orders);
    }
}
