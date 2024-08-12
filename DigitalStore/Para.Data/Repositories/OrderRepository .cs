using Microsoft.EntityFrameworkCore;
using Para.Data.Context;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.Models;

namespace Para.Data.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public async Task<Order> GetOrderWithDetailsAsync(int orderId)
    {
        return await _context.Set<Order>()
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _context.Set<Order>()
            .Include(o => o.OrderDetails)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }
}
