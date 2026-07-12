using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.promotionsEntities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories;

public class PromotionRepository : IPromotionRepository
{
    private readonly GoBiteDbContext context;

    public PromotionRepository(GoBiteDbContext context)
    {
        this.context = context;
    }

    public async Task<Promotion?> GetByIdAsync(int id)
    {
        return await context.Set<Promotion>().FindAsync(id);
    }

    public async Task<List<Promotion>> GetAllAsync()
    {
        return await context.Set<Promotion>().ToListAsync();
    }

    public async Task<List<Promotion>> GetActiveAsync()
    {
        return await context.Set<Promotion>()
            .Where(p => p.StartDate <= DateTime.UtcNow && p.EndDate >= DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task AddAsync(Promotion promotion)
    {
        await context.Set<Promotion>().AddAsync(promotion);
    }

    public Task DeleteAsync(Promotion promotion)
    {
        context.Set<Promotion>().Remove(promotion);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Set<Promotion>().AnyAsync(p => p.Id == id);
    }
}
