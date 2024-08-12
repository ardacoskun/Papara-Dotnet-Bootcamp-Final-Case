namespace Para.Schema.Entities.DTOs.Payment;

public class PaymentRequestDTO
{
    public int UserId { get; set; }
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string ExpirationDate { get; set; }
    public string CVV { get; set; }
    public string? CouponCode { get; set; }
}