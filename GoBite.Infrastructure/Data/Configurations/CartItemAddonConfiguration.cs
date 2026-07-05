using GoBite.Domain.CartEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    internal class CartItemAddonConfiguration : IEntityTypeConfiguration<CartItemAddon>
    {
        public void Configure(EntityTypeBuilder<CartItemAddon> builder)
        {
            // Table
            builder.ToTable("CartItemAddons");

            // Primary Key
            builder.HasKey(cia => cia.Id);

            // Properties
            builder.Property(cia => cia.Quantity)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(cia => cia.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(cia => cia.CartItemId);

            builder.HasIndex(cia => cia.ProductAddonId);

            // Prevent duplicate addon in the same cart item
            builder.HasIndex(cia => new
            {
                cia.CartItemId,
                cia.ProductAddonId
            }).IsUnique();

            // Relationships
            builder.HasOne(cia => cia.CartItem)
                   .WithMany(ci => ci.CartItemAddons)
                   .HasForeignKey(cia => cia.CartItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cia => cia.ProductAddon)
                   .WithMany(pa => pa.CartItemAddons)
                   .HasForeignKey(cia => cia.ProductAddonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
