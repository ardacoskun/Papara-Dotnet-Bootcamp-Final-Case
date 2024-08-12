namespace Para.Schema.Entities.Models;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CartId { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; } 

    public Product Product { get; set; }
    public Cart Cart { get; set; }
}
