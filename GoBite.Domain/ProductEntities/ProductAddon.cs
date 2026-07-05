using GoBite.Domain.CartEntities;

namespace GoBite.Domain.ProductEntities
{
    public class ProductAddon
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int AddonId { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsRequired { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public Product Product { get; set; } = null!;

        public Addon Addon { get; set; } = null!;

        public ICollection<CartItemAddon> CartItemAddons { get; set; } = new List<CartItemAddon>();
    }
}
