namespace Para.Schema.Entities.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = "User";
    public bool IsActive { get; set; } = true;  
    public decimal WalletBalance { get; set; } = 0; 
    public int PointsBalance { get; set; } = 0;    
    public ICollection<Cart> Carts { get; set; }
    public ICollection<Order> Orders { get; set; } 
}