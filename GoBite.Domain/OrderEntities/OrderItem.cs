using GoBite.Domain.ProductEntities;

namespace GoBite.Domain.OrderEntities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string? SpecialInstructions { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Order Order { get; set; } = null!;

        public Product Product { get; set; } = null!;

        public ICollection<OrderItemIngredient> OrderItemIngredients { get; set; }
            = new List<OrderItemIngredient>();

        public ICollection<OrderItemAddon> OrderItemAddons { get; set; }
            = new List<OrderItemAddon>();
    }
}
