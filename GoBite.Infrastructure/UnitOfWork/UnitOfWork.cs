using GoBite.Application.Interfaces.Rrepository;
using GoBite.Application.UnitOfWork;
using GoBite.Infrastructure.Data;

namespace GoBite.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoBiteDbContext context;

        public ICartRepository Cart { get; }
        public ICartItemRepository CartItem { get; }
        public IProductRepository Product { get; }
        public ICategoryRepository Category { get; }
        public IIngredientRepository Ingredient { get; }
        public IAddonRepository Addon { get; }
        public IProductIngredientRepository ProductIngredient { get; }
        public IProductAddonRepository ProductAddon { get; }
        public IProductPromotionRepository ProductPromotion { get; }
        public IPromotionRepository Promotion { get; }
        public ICommentRepository Comment { get; }

        public UnitOfWork(
            GoBiteDbContext context,
            ICartRepository Cart,
            ICartItemRepository CartItem,
            IProductRepository Product,
            ICategoryRepository Category,
            IIngredientRepository Ingredient,
            IAddonRepository Addon,
            IProductIngredientRepository ProductIngredient,
            IProductAddonRepository ProductAddon,
            IProductPromotionRepository ProductPromotion,
            IPromotionRepository Promotion,
            ICommentRepository Comment)
        {
            this.context = context;
            this.Cart = Cart;
            this.CartItem = CartItem;
            this.Product = Product;
            this.Category = Category;
            this.Ingredient = Ingredient;
            this.Addon = Addon;
            this.ProductIngredient = ProductIngredient;
            this.ProductAddon = ProductAddon;
            this.ProductPromotion = ProductPromotion;
            this.Promotion = Promotion;
            this.Comment = Comment;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
