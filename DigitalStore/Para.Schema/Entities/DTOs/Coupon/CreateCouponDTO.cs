namespace Para.Schema.Entities.DTOs.Coupon;

public class CreateCouponDTO
{
    public string Code { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime ExpiryDate { get; set; }
}
