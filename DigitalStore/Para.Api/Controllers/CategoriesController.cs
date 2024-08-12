using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Para.Business.Services.Interfaces;
using Para.Schema.Entities.DTOs.Category;

namespace Para.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost("add")]
    [Authorize(Roles = "Admin")] // Sadece Admin rolündeki kullanıcılar kategori ekleyebilir
    public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
    {
        var createdCategory = await _categoryService.AddCategoryAsync(createCategoryDto);
        return Ok(createdCategory); // Oluşturulan kategori Id ile birlikte döndürülür
    }

    [HttpPut("update/{id}")]
    [Authorize(Roles = "Admin")] // Sadece Admin rolündeki kullanıcılar kategoriyi güncelleyebilir
    public async Task<IActionResult> UpdateCategory(int id, CreateCategoryDto createCategoryDto)
    {
        var updatedCategory = await _categoryService.UpdateCategoryAsync(id, createCategoryDto);
        return Ok(updatedCategory);
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin")] // Sadece Admin rolündeki kullanıcılar kategoriyi silebilir
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }
}
