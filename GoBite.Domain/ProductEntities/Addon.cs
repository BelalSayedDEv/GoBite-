using GoBite.Domain.OrderEntities;

namespace GoBite.Domain.ProductEntities
{
    public class Addon
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public ICollection<ProductAddon> ProductAddons { get; set; } = new List<ProductAddon>();
        public ICollection<OrderItemAddon> OrderItemAddons { get; set; } = new List<OrderItemAddon>();
    }
}
