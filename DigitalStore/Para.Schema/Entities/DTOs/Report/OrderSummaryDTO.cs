namespace Para.Schema.Entities.DTOs.Report;

public class OrderSummaryDTO
{
    public int OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public string CouponCode { get; set; }
    public decimal CouponAmount { get; set; }
    public int PointsUsed { get; set; }
    public DateTime OrderDate { get; set; }
}
