using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Entities.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } 
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; } 
    public string CouponCode { get; set; }
    public decimal CouponAmount { get; set; }
    public int PointsUsed { get; set; } 
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
