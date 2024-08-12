namespace Para.Schema.Entities.Models;

public class PaymentDetail
{
    public int Id { get; set; }
    public int PaymentId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public Product Product { get; set; }
}
