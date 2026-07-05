using GoBite.Domain.ProductEntities;

namespace GoBite.Domain.Branch
{
    public class BranchInventory
    {
        public int Id { get; set; }

        public int BranchId { get; set; }

        public int IngredientId { get; set; }

        public decimal CurrentQuantity { get; set; }

        public decimal MinimumQuantity { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public Branch Branch { get; set; } = null!;

        public Ingredient Ingredient { get; set; } = null!;

    }
}
