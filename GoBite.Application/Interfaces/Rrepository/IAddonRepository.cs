using GoBite.Domain.ProductEntities;

namespace GoBite.Application.Interfaces.Rrepository;

public interface IAddonRepository
{
    Task<Addon?> GetByIdAsync(int id);
    Task<List<Addon>> GetAllAsync();
    Task<List<Addon>> GetActiveAsync();
    Task AddAsync(Addon addon);
    Task DeleteAsync(Addon addon);
    Task<bool> ExistsAsync(int id);
}
