using GoBite.Domain.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    internal class OrderItemIngredientConfiguration : IEntityTypeConfiguration<OrderItemIngredient>
    {
        public void Configure(EntityTypeBuilder<OrderItemIngredient> builder)
        {
            // Table
            builder.ToTable("OrderItemIngredients");

            // Primary Key
            builder.HasKey(oii => oii.Id);

            // Properties
            builder.Property(oii => oii.IngredientName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(oii => oii.DisplayOrder)
                   .HasDefaultValue(0);

            builder.Property(oii => oii.IsRemoved)
                   .HasDefaultValue(false);

            builder.Property(oii => oii.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(oii => oii.OrderItemId);

            builder.HasIndex(oii => oii.IngredientId);

            // Prevent duplicate ingredient per order item
            builder.HasIndex(oii => new
            {
                oii.OrderItemId,
                oii.IngredientId
            }).IsUnique();

            // Relationships
            builder.HasOne(oii => oii.OrderItem)
                   .WithMany(oi => oi.OrderItemIngredients)
                   .HasForeignKey(oii => oii.OrderItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oii => oii.Ingredient)
                   .WithMany(i => i.OrderItemIngredients)
                   .HasForeignKey(oii => oii.IngredientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
}
