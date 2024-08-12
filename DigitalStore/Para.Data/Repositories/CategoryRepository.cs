using Microsoft.EntityFrameworkCore;
using Para.Data.Context;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.Models;

namespace Para.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Set<Category>().ToListAsync();
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Set<Category>()
                .FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithSubcategoriesAsync()
        {
            return await _context.Set<Category>()
                .Include(c => c.Products)
                .ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetAllWithProductsAsync()
        {
            return await _context.Set<Category>()
                .Include(c => c.Products)
                .ToListAsync();
        }
    }
}
