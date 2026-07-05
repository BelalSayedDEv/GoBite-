using GoBite.Domain.Entities;

namespace GoBite.Domain.OrderEntities
{
    public class Order
    {
        public int Id { get; set; }

        public int? BranchId { get; set; }

        public int CustomerId { get; set; }

        public int OrderStatusId { get; set; }

        public int CustomerAddressId { get; set; }

        public decimal Subtotal { get; set; }

        public decimal DeliveryFees { get; set; }

        public decimal TotalPrice { get; set; }

        public bool CustomerConfirmed { get; set; }

        public DateTime? PlacedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Properties

        public ApplicationUser Customer { get; set; } = null!;

        public OrderStatus OrderStatus { get; set; } = null!;

        public ICollection<OrderItem> OrderItems { get; set; }
            = new List<OrderItem>();
    }
}
