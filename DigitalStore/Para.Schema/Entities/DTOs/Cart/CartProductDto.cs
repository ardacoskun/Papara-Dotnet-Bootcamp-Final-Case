namespace Para.Schema.Entities.DTOs.Cart;

public class CartProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int Quantity { get; set; }
}
