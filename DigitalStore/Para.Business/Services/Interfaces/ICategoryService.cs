using Para.Schema.Entities.DTOs.Category;

namespace Para.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> AddCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<CategoryDto> UpdateCategoryAsync(int id, CreateCategoryDto createCategoryDto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    }
}
