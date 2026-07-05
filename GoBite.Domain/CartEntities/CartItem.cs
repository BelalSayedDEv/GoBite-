using GoBite.Domain.ProductEntities;

namespace GoBite.Domain.CartEntities
{
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;

        public string? SpecialInstructions { get; set; }

        public DateTime CreatedAt { get; set; }

        public Cart Cart { get; set; } = null!;

        public Product Product { get; set; } = null!;

        public ICollection<CartItemRemovedIngredient> CartItemRemoveds { get; set; } = new List<CartItemRemovedIngredient>();
        public ICollection<CartItemAddon> CartItemAddons { get; set; } = new List<CartItemAddon>();


    }

}
