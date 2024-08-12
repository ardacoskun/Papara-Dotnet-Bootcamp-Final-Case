using Microsoft.EntityFrameworkCore;
using Para.Data.Context;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.Models;

namespace Para.Data.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context) { }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Set<Cart>()
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                cart.CartItems.Clear(); 
                _context.Set<Cart>().Update(cart); 
                await _context.SaveChangesAsync(); 
            }
        }
    }
}
