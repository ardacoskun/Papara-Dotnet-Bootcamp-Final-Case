using Para.Schema.Entities.DTOs.Cart;

namespace Para.Business.Services.Interfaces;

public interface ICartService
{
    Task<CartDTO> GetCartByUserIdAsync(int userId);
    Task<CartDTO> AddToCartAsync(int userId, CreateCartDTO cartItemDto);
    Task<CartDTO> UpdateCartItemAsync(int userId, CreateCartDTO cartItemDto);
    Task RemoveFromCartAsync(int userId, int productId);
    Task ClearCartAsync(int userId);
}
