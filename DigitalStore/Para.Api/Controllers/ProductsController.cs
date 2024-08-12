using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Para.Business.Services.Interfaces;
using Para.Schema.Entities.DTOs.Product;

namespace Para.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(CreateProductDto createProductDto)
        {
            var createdProduct = await _productService.AddProductAsync(createProductDto);
            return Ok(createdProduct);

        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, CreateProductDto createProductDto)
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, createProductDto);
            return Ok(updatedProduct);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] string? search = null,
            [FromQuery] string? sortBy = "asc", // Varsayılan sıralama yönü ascending olarak ayarlanmıştır
            [FromQuery] string? sortByField = "id", // Varsayılan sıralama alanı id olarak ayarlanmıştır
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetAllProductsAsync(search, sortBy, sortByField, page, pageSize);
            return Ok(products);
        }

    }
