using GoBite.Domain.promotionsEntities;

namespace GoBite.Application.Interfaces.Rrepository;

public interface IPromotionRepository
{
    Task<Promotion?> GetByIdAsync(int id);
    Task<List<Promotion>> GetAllAsync();
    Task<List<Promotion>> GetActiveAsync();
    Task AddAsync(Promotion promotion);
    Task DeleteAsync(Promotion promotion);
    Task<bool> ExistsAsync(int id);
}
