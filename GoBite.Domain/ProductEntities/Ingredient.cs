using GoBite.Domain.CartEntities;
using GoBite.Domain.OrderEntities;

namespace GoBite.Domain.ProductEntities
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();

        //public ICollection<BranchInventory> BranchInventories { get; set; } = null!; for future
        public ICollection<CartItemRemovedIngredient> CartItemRemoveds { get; set; } = new List<CartItemRemovedIngredient>();

        public ICollection<OrderItemIngredient> OrderItemIngredients { get; set; } = new List<OrderItemIngredient>();


    }
}
