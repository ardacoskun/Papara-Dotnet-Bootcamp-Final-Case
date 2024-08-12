using Para.Schema.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Data.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetOrderWithDetailsAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
}
