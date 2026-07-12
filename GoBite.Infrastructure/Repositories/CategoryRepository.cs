using GoBite.Application.Contracts;
using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.ProductEntities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly GoBiteDbContext context;

    public CategoryRepository(GoBiteDbContext context)
    {
        this.context = context;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await context.Set<Category>().FindAsync(id);
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await context.Set<Category>()
            .OrderBy(c => c.DisplayOrder)
            .ToListAsync();
    }

    public async Task<List<Category>> GetActiveAsync()
    {
        return await context.Set<Category>()
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .ToListAsync();
    }

    public async Task<PaginationResult<Category>> GetPagedAsync(int page, int pageSize)
    {
        var query = context.Set<Category>().OrderBy(c => c.DisplayOrder);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginationResult<Category>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task AddAsync(Category category)
    {
        await context.Set<Category>().AddAsync(category);
    }

    public Task DeleteAsync(Category category)
    {
        context.Set<Category>().Remove(category);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Set<Category>().AnyAsync(c => c.Id == id);
    }
}
