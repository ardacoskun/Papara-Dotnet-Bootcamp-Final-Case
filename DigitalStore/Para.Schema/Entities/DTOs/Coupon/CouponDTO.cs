namespace Para.Schema.Entities.DTOs.Coupon;

public class CouponDTO
{
    public int Id { get; set; }
    public string Code { get; set; }
    public decimal DiscountAmount { get; set; }
    public bool IsActive { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiryDate { get; set; }
}
