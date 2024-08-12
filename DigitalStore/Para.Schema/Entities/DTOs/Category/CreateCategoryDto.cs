using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Schema.Entities.DTOs.Category;

public class CreateCategoryDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; } 
    public List<string> Tags { get; set; } = new List<string>();
}
