using AutoMapper;
using Para.Business.Services.Interfaces;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.DTOs.Category;
using Para.Schema.Entities.Models;

namespace Para.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDto> AddCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, CreateCategoryDto createCategoryDto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            _mapper.Map(createCategoryDto, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetAllWithProductsAsync(); 
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllWithProductsAsync(); 

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
}
