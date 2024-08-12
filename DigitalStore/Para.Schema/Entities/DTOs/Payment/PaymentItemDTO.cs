namespace Para.Schema.Entities.DTOs.Payment;

public class PaymentItemDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
}