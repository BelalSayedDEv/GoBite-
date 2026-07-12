using GoBite.Application.Interfaces.Rrepository;
using GoBite.Domain.PromotionEntities;
using GoBite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Repositories;

public class ProductPromotionRepository : IProductPromotionRepository
{
    private readonly GoBiteDbContext context;

    public ProductPromotionRepository(GoBiteDbContext context)
    {
        this.context = context;
    }

    public async Task<ProductPromotion?> GetByIdAsync(int id)
    {
        return await context.Set<ProductPromotion>().FindAsync(id);
    }

    public async Task<List<ProductPromotion>> GetByProductIdAsync(int productId)
    {
        return await context.Set<ProductPromotion>()
            .Where(pp => pp.ProductId == productId)
            .ToListAsync();
    }

    public async Task<List<ProductPromotion>> GetByPromotionIdAsync(int promotionId)
    {
        return await context.Set<ProductPromotion>()
            .Where(pp => pp.PromotionId == promotionId)
            .ToListAsync();
    }

    public async Task AddAsync(ProductPromotion productPromotion)
    {
        await context.Set<ProductPromotion>().AddAsync(productPromotion);
    }

    public Task DeleteAsync(ProductPromotion productPromotion)
    {
        context.Set<ProductPromotion>().Remove(productPromotion);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Set<ProductPromotion>().AnyAsync(pp => pp.Id == id);
    }
}
