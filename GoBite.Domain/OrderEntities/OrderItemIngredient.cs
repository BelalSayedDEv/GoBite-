using GoBite.Domain.ProductEntities;

namespace GoBite.Domain.OrderEntities
{
    public class OrderItemIngredient
    {
        public int Id { get; set; }

        public int OrderItemId { get; set; }

        public int IngredientId { get; set; }

        public string IngredientName { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public OrderItem OrderItem { get; set; } = null!;

        public Ingredient Ingredient { get; set; } = null!;
    }
}
