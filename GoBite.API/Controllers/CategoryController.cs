using GoBite.API.Model;
using GoBite.Application.Contracts;
using GoBite.Application.DTOs.Categories;
using GoBite.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace GoBite.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await categoryService.GetAllAsync();
        if (!result.IsSuccess)
            return NotFound(ApiResponse<List<CategoryDto>>.Failure("Categories not found"));
        return Ok(ApiResponse<List<CategoryDto>>.Success(result.Value));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var result = await categoryService.GetActiveAsync();
        if (!result.IsSuccess)
            return NotFound(ApiResponse<List<CategoryDto>>.Failure("Active categories not found"));
        return Ok(ApiResponse<List<CategoryDto>>.Success(result.Value));
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await categoryService.GetPagedAsync(page, pageSize);
        if (!result.IsSuccess)
            return NotFound(ApiResponse<PaginationResult<CategoryDto>>.Failure("Categories not found"));
        return Ok(ApiResponse<PaginationResult<CategoryDto>>.Success(result.Value));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await categoryService.GetByIdAsync(id);
        if (!result.IsSuccess)
            return NotFound(ApiResponse<CategoryDto?>.Failure("Category not found"));
        return Ok(ApiResponse<CategoryDto?>.Success(result.Value));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        var result = await categoryService.CreateAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(ApiResponse<CategoryDto>.Failure("Failed to create category", result.Errors?.ToList()));
        return CreatedAtAction(nameof(GetById), new { id = result.Value?.Id }, ApiResponse<CategoryDto>.Success(result.Value));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
    {
        var result = await categoryService.UpdateAsync(id, dto);
        if (!result.IsSuccess)
            return NotFound(ApiResponse<CategoryDto>.Failure("Category not found"));
        return Ok(ApiResponse<CategoryDto>.Success(result.Value));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await categoryService.DeleteAsync(id);
        if (!result.IsSuccess)
            return NotFound(ApiResponse<object>.Failure("Category not found"));
        return Ok(ApiResponse<object>.Success(null, "Category deleted successfully"));
    }
}
