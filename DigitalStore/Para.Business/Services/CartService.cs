using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Para.Business.Services.Interfaces;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.DTOs.Cart;
using Para.Schema.Entities.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Para.Business.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartDTO> GetCartByUserIdAsync(int userId)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);

            if (cart == null || !cart.CartItems.Any())
            {
                return new CartDTO
                {
                    UserId = userId,
                    CartItems = new List<CartProductDto>(),
                    TotalAmount = 0m
                };
            }

            // Category bilgilerini içeren ürünleri dahil ederek mapleme yapıyoruz
            foreach (var item in cart.CartItems)
            {
                item.Product = await _unitOfWork.Products
                    .GetAllWithCategories() // GetAllWithCategories yöntemini kullanıyoruz
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId);
            }

            var cartDto = _mapper.Map<CartDTO>(cart);
            cartDto.TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);

            return cartDto;
        }

        public async Task<CartDTO> AddToCartAsync(int userId, CreateCartDTO createCartDto)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                await _unitOfWork.Carts.AddAsync(cart);
            }

            var product = await _unitOfWork.Products
                .GetAllWithCategories() 
                .FirstOrDefaultAsync(p => p.Id == createCartDto.ProductId);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            if (product.StockQuantity < createCartDto.Quantity)
                throw new InvalidOperationException($"The requested quantity exceeds the available stock for product {product.Name}. Available stock: {product.StockQuantity}");

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == createCartDto.ProductId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += createCartDto.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = createCartDto.Quantity
                };
                cart.CartItems.Add(cartItem);
            }

            await _unitOfWork.CommitAsync();

            var cartDto = _mapper.Map<CartDTO>(cart);
            cartDto.TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);

            return cartDto;
        }

        public async Task<CartDTO> UpdateCartItemAsync(int userId, CreateCartDTO createCartDto)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == createCartDto.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity = createCartDto.Quantity;

                cartItem.Product = await _unitOfWork.Products
                    .GetAllWithCategories() 
                    .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

                _unitOfWork.Carts.Update(cart);
                await _unitOfWork.CommitAsync();
            }

            var cartDto = _mapper.Map<CartDTO>(cart);
            cartDto.TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);

            return cartDto;
        }

        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                _unitOfWork.Carts.Update(cart);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await _unitOfWork.Carts.GetCartByUserIdAsync(userId);
            cart.CartItems.Clear();
            _unitOfWork.Carts.Update(cart);
            await _unitOfWork.CommitAsync();
        }
    }
}
