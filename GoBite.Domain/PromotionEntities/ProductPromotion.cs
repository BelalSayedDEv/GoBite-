using GoBite.Domain.ProductEntities;
using GoBite.Domain.promotionsEntities;

namespace GoBite.Domain.PromotionEntities
{
    public class ProductPromotion
    {

        public int Id { get; set; }

        public int ProductId { get; set; }

        public int PromotionId { get; set; }

        // Navigation Properties
        public Product Product { get; set; } = null!;

        public Promotion Promotion { get; set; } = null!;
    }
}
