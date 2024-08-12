using Para.Schema.Entities.Models;

namespace Para.Data.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllAsync(); 
        Task<Category> GetCategoryByNameAsync(string categoryName);
        Task<IEnumerable<Category>> GetCategoriesWithSubcategoriesAsync();
        Task<IEnumerable<Category>> GetAllWithProductsAsync();
    }
}
