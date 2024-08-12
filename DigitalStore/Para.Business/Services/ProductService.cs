using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Para.Business.Services.Interfaces;
using Para.Data.Repositories.Interfaces;
using Para.Schema.Entities.DTOs.Product;
using Para.Schema.Entities.Models;


namespace Para.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddProductAsync(CreateProductDto createProductDto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(createProductDto.CategoryId);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            var product = _mapper.Map<Product>(createProductDto);
            product.Category = category;

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, CreateProductDto createProductDto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            var category = await _unitOfWork.Categories.GetByIdAsync(createProductDto.CategoryId);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            _mapper.Map(createProductDto, product);
            product.Category = category;
            product.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Products.Update(product);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products
                .GetAllWithCategories()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(string? search, string sortBy, string sortByField, int page, int pageSize)
        {
            var isDescending = sortBy?.ToLower() == "desc";
            var products = await _unitOfWork.Products
                .GetFilteredProductsAsync(search, sortByField, isDescending, page, pageSize);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
