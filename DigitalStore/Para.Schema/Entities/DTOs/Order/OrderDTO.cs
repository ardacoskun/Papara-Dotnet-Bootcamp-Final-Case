using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Entities.DTOs.Order;

public class OrderDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Amount { get; set; }
    public decimal DiscountedAmount { get; set; } 
    public string CouponCode { get; set; }
    public decimal CouponAmount { get; set; }
    public int PointsUsed { get; set; }
    public List<OrderDetailDTO> OrderDetails { get; set; } 
}
