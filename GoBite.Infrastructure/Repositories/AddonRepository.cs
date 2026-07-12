using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.ProductEntities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories;

public class AddonRepository : IAddonRepository
{
    private readonly GoBiteDbContext context;

    public AddonRepository(GoBiteDbContext context)
    {
        this.context = context;
    }

    public async Task<Addon?> GetByIdAsync(int id)
    {
        return await context.Set<Addon>().FindAsync(id);
    }

    public async Task<List<Addon>> GetAllAsync()
    {
        return await context.Set<Addon>().ToListAsync();
    }

    public async Task<List<Addon>> GetActiveAsync()
    {
        return await context.Set<Addon>()
            .Where(a => a.IsActive)
            .ToListAsync();
    }

    public async Task AddAsync(Addon addon)
    {
        await context.Set<Addon>().AddAsync(addon);
    }

    public Task DeleteAsync(Addon addon)
    {
        context.Set<Addon>().Remove(addon);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Set<Addon>().AnyAsync(a => a.Id == id);
    }
}
