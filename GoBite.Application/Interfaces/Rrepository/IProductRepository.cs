using GoBite.Domain.ProductEntities;

namespace GoBite.Application.Interfaces.Rrepository;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetByCategoryIdAsync(int categoryId);
    Task<List<Product>> GetAvailableAsync();
    Task<List<Product>> SearchByNameAsync(string name);
    Task AddAsync(Product product);
    Task DeleteAsync(Product product);
    Task<bool> ExistsAsync(int id);
}
