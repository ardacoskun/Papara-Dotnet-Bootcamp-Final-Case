using Para.Schema.Entities.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Business.Services.Interfaces;

public interface IOrderService
{
    Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO);
    Task<OrderDTO> GetOrderByIdAsync(int orderId);
    Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int userId);
}
