using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.ProductEntities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories;

public class ProductIngredientRepository : IProductIngredientRepository
{
    private readonly GoBiteDbContext context;

    public ProductIngredientRepository(GoBiteDbContext context)
    {
        this.context = context;
    }

    public async Task<ProductIngredient?> GetByIdAsync(int id)
    {
        return await context.Set<ProductIngredient>().FindAsync(id);
    }

    public async Task<List<ProductIngredient>> GetByProductIdAsync(int productId)
    {
        return await context.Set<ProductIngredient>()
            .Where(pi => pi.ProductId == productId)
            .OrderBy(pi => pi.DisplayOrder)
            .ToListAsync();
    }

    public async Task<List<ProductIngredient>> GetByIngredientIdAsync(int ingredientId)
    {
        return await context.Set<ProductIngredient>()
            .Where(pi => pi.IngredientId == ingredientId)
            .ToListAsync();
    }

    public async Task AddAsync(ProductIngredient productIngredient)
    {
        await context.Set<ProductIngredient>().AddAsync(productIngredient);
    }

    public Task DeleteAsync(ProductIngredient productIngredient)
    {
        context.Set<ProductIngredient>().Remove(productIngredient);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Set<ProductIngredient>().AnyAsync(pi => pi.Id == id);
    }
}
