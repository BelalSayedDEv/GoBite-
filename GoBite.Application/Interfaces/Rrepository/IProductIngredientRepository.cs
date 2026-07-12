using GoBite.Domain.ProductEntities;

namespace GoBite.Application.Interfaces.Rrepository;

public interface IProductIngredientRepository
{
    Task<ProductIngredient?> GetByIdAsync(int id);
    Task<List<ProductIngredient>> GetByProductIdAsync(int productId);
    Task<List<ProductIngredient>> GetByIngredientIdAsync(int ingredientId);
    Task AddAsync(ProductIngredient productIngredient);
    Task DeleteAsync(ProductIngredient productIngredient);
    Task<bool> ExistsAsync(int id);
}
