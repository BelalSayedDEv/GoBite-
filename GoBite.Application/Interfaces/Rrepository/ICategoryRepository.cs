using GoBite.Application.Contracts;
using GoBite.Domain.ProductEntities;

namespace GoBite.Application.Interfaces.Rrepository;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(int id);
    Task<List<Category>> GetAllAsync();
    Task<List<Category>> GetActiveAsync();
    Task<PaginationResult<Category>> GetPagedAsync(int page, int pageSize);
    Task AddAsync(Category category);
    Task DeleteAsync(Category category);
    Task<bool> ExistsAsync(int id);
}
