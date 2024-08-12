namespace Para.Schema.Entities.DTOs.Cart;

public class CartDTO
{
    public int UserId { get; set; }
    public List<CartProductDto> CartItems { get; set; }
    public decimal TotalAmount { get; set; }
}