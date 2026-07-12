using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.ProductEntities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories;

public class ProductAddonRepository : IProductAddonRepository
{
    private readonly GoBiteDbContext context;

    public ProductAddonRepository(GoBiteDbContext context)
    {
        this.context = context;
    }

    public async Task<ProductAddon?> GetByIdAsync(int id)
    {
        return await context.Set<ProductAddon>().FindAsync(id);
    }

    public async Task<List<ProductAddon>> GetByProductIdAsync(int productId)
    {
        return await context.Set<ProductAddon>()
            .Where(pa => pa.ProductId == productId)
            .OrderBy(pa => pa.DisplayOrder)
            .ToListAsync();
    }

    public async Task<List<ProductAddon>> GetByAddonIdAsync(int addonId)
    {
        return await context.Set<ProductAddon>()
            .Where(pa => pa.AddonId == addonId)
            .ToListAsync();
    }

    public async Task<List<ProductAddon>> GetActiveByProductIdAsync(int productId)
    {
        return await context.Set<ProductAddon>()
            .Where(pa => pa.ProductId == productId && pa.IsActive)
            .OrderBy(pa => pa.DisplayOrder)
            .ToListAsync();
    }

    public async Task AddAsync(ProductAddon productAddon)
    {
        await context.Set<ProductAddon>().AddAsync(productAddon);
    }

    public Task DeleteAsync(ProductAddon productAddon)
    {
        context.Set<ProductAddon>().Remove(productAddon);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Set<ProductAddon>().AnyAsync(pa => pa.Id == id);
    }
}
