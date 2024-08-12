using Para.Schema.Entities;

namespace Para.Schema.Entities.DTOs.Order;

public class OrderDetailDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
}