using GoBite.Domain.ProductEntities;

namespace GoBite.Domain.CartEntities
{
    public class CartItemRemovedIngredient
    {
        public int Id { get; set; }

        public int CartItemId { get; set; }

        public int IngredientId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public CartItem CartItem { get; set; } = null!;

        public Ingredient Ingredient { get; set; } = null!;
    }
}
