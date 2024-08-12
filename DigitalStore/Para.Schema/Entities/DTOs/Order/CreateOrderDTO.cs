namespace Para.Schema.Entities.DTOs.Order;

public class CreateOrderDTO
{
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string CouponCode { get; set; }
    public decimal CouponAmount { get; set; }
    public int PointsUsed { get; set; }
    public List<OrderDetailDTO> OrderDetails { get; set; }
}
