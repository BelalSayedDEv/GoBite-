using GoBite.Domain.ProductEntities;

namespace GoBite.Domain.OrderEntities
{
    public class OrderItemAddon
    {
        public int Id { get; set; }

        public int OrderItemId { get; set; }

        public int AddonId { get; set; }

        public string AddonName { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public OrderItem OrderItem { get; set; } = null!;

        public Addon Addon { get; set; } = null!;
    }
}
