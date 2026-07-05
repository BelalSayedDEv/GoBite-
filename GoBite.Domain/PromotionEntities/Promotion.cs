using GoBite.Domain.Enums;
using GoBite.Domain.PromotionEntities;

namespace GoBite.Domain.promotionsEntities
{
    public class Promotion
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public DiscountType DiscountType { get; set; }

        public decimal DiscountValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ICollection<ProductPromotion> ProductPromotions { get; set; }
            = new List<ProductPromotion>();
    }
}
