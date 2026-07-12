using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.ProductEntities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories;

public class IngredientRepository : IIngredientRepository
{
    private readonly GoBiteDbContext context;

    public IngredientRepository(GoBiteDbContext context)
    {
        this.context = context;
    }

    public async Task<Ingredient?> GetByIdAsync(int id)
    {
        return await context.Set<Ingredient>().FindAsync(id);
    }

    public async Task<List<Ingredient>> GetAllAsync()
    {
        return await context.Set<Ingredient>()
            .OrderBy(i => i.DisplayOrder)
            .ToListAsync();
    }

    public async Task<List<Ingredient>> GetActiveAsync()
    {
        return await context.Set<Ingredient>()
            .Where(i => i.IsActive)
            .OrderBy(i => i.DisplayOrder)
            .ToListAsync();
    }

    public async Task AddAsync(Ingredient ingredient)
    {
        await context.Set<Ingredient>().AddAsync(ingredient);
    }

    public Task DeleteAsync(Ingredient ingredient)
    {
        context.Set<Ingredient>().Remove(ingredient);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Set<Ingredient>().AnyAsync(i => i.Id == id);
    }
}
