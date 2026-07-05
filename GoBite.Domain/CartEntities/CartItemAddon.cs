using GoBite.Domain.ProductEntities;

namespace GoBite.Domain.CartEntities
{
    public class CartItemAddon
    {
        public int Id { get; set; }

        public int CartItemId { get; set; }

        public int ProductAddonId { get; set; }

        public int Quantity { get; set; } = 1;

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public CartItem CartItem { get; set; } = null!;

        public ProductAddon ProductAddon { get; set; } = null!;
    }
}
