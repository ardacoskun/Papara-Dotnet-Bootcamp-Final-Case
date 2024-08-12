using Para.Schema.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Para.Data.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetAllWithCategories();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Product>> GetFilteredProductsAsync(string search, string sortBy, bool isDescending, int page, int pageSize);
    }
}
