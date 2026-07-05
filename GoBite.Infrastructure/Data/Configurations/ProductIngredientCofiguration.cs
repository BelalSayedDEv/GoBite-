using GoBite.Domain.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class ProductIngredientCofiguration : IEntityTypeConfiguration<ProductIngredient>
    {
        public void Configure(EntityTypeBuilder<ProductIngredient> builder)
        {
            // Table
            builder.ToTable("ProductIngredients");

            // Primary Key
            builder.HasKey(pi => pi.Id);

            // Properties
            builder.Property(pi => pi.DisplayOrder)
                   .HasDefaultValue(0);

            builder.Property(pi => pi.IsRemovable)
                   .HasDefaultValue(false);

            builder.Property(pi => pi.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(pi => pi.ProductId);

            builder.HasIndex(pi => pi.IngredientId);

            // Prevent duplicate ingredient for same product
            builder.HasIndex(pi => new
            {
                pi.ProductId,
                pi.IngredientId
            }).IsUnique();

            // Relationships
            builder.HasOne(pi => pi.Product)
                   .WithMany(p => p.ProductIngredients)
                   .HasForeignKey(pi => pi.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pi => pi.Ingredient)
                   .WithMany(i => i.ProductIngredients)
                   .HasForeignKey(pi => pi.IngredientId)
                   .OnDelete(DeleteBehavior.Restrict);
            // Restrict her prevent the deleation of any ingradiant without delete product before
        }
    }
}
