using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Entities.Models;

public class Coupon
{
    public int Id { get; set; }
    public string Code { get; set; }
    public decimal DiscountAmount { get; set; }
    public bool IsActive { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiryDate { get; set; }
}
