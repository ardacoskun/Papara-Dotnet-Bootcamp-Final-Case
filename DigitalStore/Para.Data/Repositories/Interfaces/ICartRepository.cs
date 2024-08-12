using Para.Schema.Entities.Models;

namespace Para.Data.Repositories.Interfaces;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetCartByUserIdAsync(int userId);
    Task ClearCartAsync(int userId);
}