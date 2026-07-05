namespace GoBite.Domain.ProductEntities
{
    public class ProductIngredient
    {
        public int Id { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsRemovable { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; } = null!;


    }
}
