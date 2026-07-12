using Ardalis.Result;
using GoBite.Application.DTOs.Categories;
using GoBite.Application.Interfaces.Service;
using GoBite.Application.UnitOfWork;
using GoBite.Domain.ProductEntities;
using GoBite.Application.Contracts;

namespace GoBite.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<CategoryDto?>> GetByIdAsync(int id)
    {
        var category = await unitOfWork.Category.GetByIdAsync(id);

        if (category is null)
            return Result<CategoryDto?>.NotFound();

        return Result<CategoryDto?>.Success(Map(category));
    }

    public async Task<Result<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await unitOfWork.Category.GetAllAsync();
        var dtos = categories.Select(Map).ToList();
        return Result<List<CategoryDto>>.Success(dtos);
    }

    public async Task<Result<List<CategoryDto>>> GetActiveAsync()
    {
        var categories = await unitOfWork.Category.GetActiveAsync();
        var dtos = categories.Select(Map).ToList();
        return Result<List<CategoryDto>>.Success(dtos);
    }

    public async Task<Result<PaginationResult<CategoryDto>>> GetPagedAsync(int page, int pageSize)
    {
        var paged = await unitOfWork.Category.GetPagedAsync(page, pageSize);

        var dtoItems = paged.Items.Select(Map).ToList();

        var result = new GoBite.Application.Contracts.PaginationResult<CategoryDto>
        {
            Items = dtoItems,
            Page = paged.Page,
            PageSize = paged.PageSize,
            TotalCount = paged.TotalCount
        };

        return Result<PaginationResult<CategoryDto>>.Success(result);
    }

    public async Task<Result<CategoryDto>> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            ImageUrl = dto.ImageUrl,
            DisplayOrder = dto.DisplayOrder,
            IsActive = true
        };

        await unitOfWork.Category.AddAsync(category);
        await unitOfWork.SaveAsync();

        return Result<CategoryDto>.Success(Map(category));
    }

    public async Task<Result<CategoryDto>> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await unitOfWork.Category.GetByIdAsync(id);

        if (category is null)
            return Result<CategoryDto>.NotFound();

        category.Name = dto.Name;
        category.ImageUrl = dto.ImageUrl;
        category.DisplayOrder = dto.DisplayOrder;
        category.IsActive = dto.IsActive;

        await unitOfWork.SaveAsync();

        return Result<CategoryDto>.Success(Map(category));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var category = await unitOfWork.Category.GetByIdAsync(id);

        if (category is null)
            return Result.NotFound();

        await unitOfWork.Category.DeleteAsync(category);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    private static CategoryDto Map(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl,
            DisplayOrder = category.DisplayOrder,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt
        };
    }
}
