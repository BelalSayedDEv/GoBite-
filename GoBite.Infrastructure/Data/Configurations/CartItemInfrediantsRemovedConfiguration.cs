using GoBite.Domain.CartEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class CartItemInfrediantsRemovedConfiguration : IEntityTypeConfiguration<CartItemRemovedIngredient>
    {
        public void Configure(EntityTypeBuilder<CartItemRemovedIngredient> builder)
        {
            // Table
            builder.ToTable("CartItemRemovedIngredients");

            // Primary Key
            builder.HasKey(cri => cri.Id);

            // Properties
            builder.Property(cri => cri.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(cri => cri.CartItemId);

            builder.HasIndex(cri => cri.IngredientId);

            // Prevent duplicate removed ingredient
            builder.HasIndex(cri => new
            {
                cri.CartItemId,
                cri.IngredientId
            }).IsUnique();

            // Relationships
            builder.HasOne(cri => cri.CartItem)
                   .WithMany(ci => ci.CartItemRemoveds)
                   .HasForeignKey(cri => cri.CartItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cri => cri.Ingredient)
                   .WithMany(i => i.CartItemRemoveds)
                   .HasForeignKey(cri => cri.IngredientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
