using Para.Schema.Entities.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; } 
    public List<string> Tags { get; set; } = new List<string>();
    public ICollection<Product> Products { get; set; }
}