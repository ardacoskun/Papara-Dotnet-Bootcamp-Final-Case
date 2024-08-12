using Para.Schema.Entities.DTOs.Product;

namespace Para.Schema.Entities.DTOs.Category;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; } 
    public List<string> Tags { get; set; } = new List<string>();
    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>(); 
}
