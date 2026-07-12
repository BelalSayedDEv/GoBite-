using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.ProductEntities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly GoBiteDbContext context;

    public ProductRepository(GoBiteDbContext context)
    {
        this.context = context;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await context.Set<Product>().FindAsync(id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await context.Set<Product>().ToListAsync();
    }

    public async Task<List<Product>> GetByCategoryIdAsync(int categoryId)
    {
        return await context.Set<Product>()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<List<Product>> GetAvailableAsync()
    {
        return await context.Set<Product>()
            .Where(p => p.IsAvailable && p.IsActive)
            .ToListAsync();
    }

    public async Task<List<Product>> SearchByNameAsync(string name)
    {
        return await context.Set<Product>()
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await context.Set<Product>().AddAsync(product);
    }

    public Task DeleteAsync(Product product)
    {
        context.Set<Product>().Remove(product);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Set<Product>().AnyAsync(p => p.Id == id);
    }
}
