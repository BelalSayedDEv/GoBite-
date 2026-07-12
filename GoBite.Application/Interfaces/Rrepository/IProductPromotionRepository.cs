using GoBite.Domain.PromotionEntities;

namespace GoBite.Application.Interfaces.Rrepository;

public interface IProductPromotionRepository
{
    Task<ProductPromotion?> GetByIdAsync(int id);
    Task<List<ProductPromotion>> GetByProductIdAsync(int productId);
    Task<List<ProductPromotion>> GetByPromotionIdAsync(int promotionId);
    Task AddAsync(ProductPromotion productPromotion);
    Task DeleteAsync(ProductPromotion productPromotion);
    Task<bool> ExistsAsync(int id);
}
