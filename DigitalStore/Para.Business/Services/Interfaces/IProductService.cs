using Para.Schema.Entities.DTOs.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Para.Business.Services.Interfaces;
public interface IProductService
{
    Task<ProductDto> AddProductAsync(CreateProductDto createProductDto);
    Task<ProductDto> UpdateProductAsync(int id, CreateProductDto createProductDto);
    Task<bool> DeleteProductAsync(int id);
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? search, string sortBy, string sortByField, int page, int pageSize); // Yeni parametreler eklendi
}
