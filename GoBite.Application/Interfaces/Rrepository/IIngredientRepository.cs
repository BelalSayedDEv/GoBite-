using GoBite.Domain.ProductEntities;

namespace GoBite.Application.Interfaces.Rrepository;

public interface IIngredientRepository
{
    Task<Ingredient?> GetByIdAsync(int id);
    Task<List<Ingredient>> GetAllAsync();
    Task<List<Ingredient>> GetActiveAsync();
    Task AddAsync(Ingredient ingredient);
    Task DeleteAsync(Ingredient ingredient);
    Task<bool> ExistsAsync(int id);
}
