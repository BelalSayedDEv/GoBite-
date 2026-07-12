using GoBite.Domain.ProductEntities;

namespace GoBite.Application.Interfaces.Rrepository;

public interface IProductAddonRepository
{
    Task<ProductAddon?> GetByIdAsync(int id);
    Task<List<ProductAddon>> GetByProductIdAsync(int productId);
    Task<List<ProductAddon>> GetByAddonIdAsync(int addonId);
    Task<List<ProductAddon>> GetActiveByProductIdAsync(int productId);
    Task AddAsync(ProductAddon productAddon);
    Task DeleteAsync(ProductAddon productAddon);
    Task<bool> ExistsAsync(int id);
}
