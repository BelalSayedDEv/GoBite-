using Ardalis.Result;
using GoBite.Application.DTOs.Categories;

namespace GoBite.Application.Interfaces.Service;

public interface ICategoryService
{
    Task<Result<CategoryDto?>> GetByIdAsync(int id);
    Task<Result<List<CategoryDto>>> GetAllAsync();
    Task<Result<List<CategoryDto>>> GetActiveAsync();
    Task<Result<GoBite.Application.Contracts.PaginationResult<CategoryDto>>> GetPagedAsync(int page, int pageSize);
    Task<Result<CategoryDto>> CreateAsync(CreateCategoryDto dto);
    Task<Result<CategoryDto>> UpdateAsync(int id, UpdateCategoryDto dto);
    Task<Result> DeleteAsync(int id);
}
