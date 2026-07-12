using GoBite.Application.Interfaces.Rrepository;

namespace GoBite.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICartRepository Cart { get; }
        ICartItemRepository CartItem { get; }
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IIngredientRepository Ingredient { get; }
        IAddonRepository Addon { get; }
        IProductIngredientRepository ProductIngredient { get; }
        IProductAddonRepository ProductAddon { get; }
        IProductPromotionRepository ProductPromotion { get; }
        IPromotionRepository Promotion { get; }
        ICommentRepository Comment { get; }
        Task SaveAsync();
    }
}
