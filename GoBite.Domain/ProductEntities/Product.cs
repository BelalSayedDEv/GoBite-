using GoBite.Domain.CartEntities;
using GoBite.Domain.CommentEntities;
using GoBite.Domain.OrderEntities;
using GoBite.Domain.PromotionEntities;

namespace GoBite.Domain.ProductEntities
{
    public class Product
    {
        public int Id { get; set; }

        public int? BranchId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal BasePrice { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public Category Category { get; set; } = null!;

        public ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();

        public ICollection<ProductAddon> ProductAddons { get; set; } = new List<ProductAddon>();

        public ICollection<CartItem> CartItems { get; set; }
           = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; }
          = new List<OrderItem>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<ProductPromotion> ProductPromotions { get; set; }
        = new List<ProductPromotion>();
    }
}
