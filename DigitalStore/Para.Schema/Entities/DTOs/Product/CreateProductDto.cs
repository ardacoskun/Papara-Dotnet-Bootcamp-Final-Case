namespace Para.Schema.Entities.DTOs.Product;

public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }  
    public decimal PointsPercentage { get; set; } 
    public decimal MaxPoints { get; set; }  

}
