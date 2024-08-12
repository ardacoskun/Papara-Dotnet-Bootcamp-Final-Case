namespace Para.Schema.Entities.Models;

public class Payment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public decimal DiscountedAmount { get; set; } 
    public decimal CouponAmount { get; set; } 
    public int PointsUsed { get; set; } 
    public string CouponName { get; set; }
    public DateTime PaymentDate { get; set; }
    public User User { get; set; }
    public ICollection<PaymentDetail> PaymentDetails { get; set; }
}
