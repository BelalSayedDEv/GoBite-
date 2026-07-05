using GoBite.Domain.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    internal class ProductAddonConfiguration : IEntityTypeConfiguration<ProductAddon>
    {
        public void Configure(EntityTypeBuilder<ProductAddon> builder)
        {
            // Table
            builder.ToTable("ProductAddons");

            // Primary Key
            builder.HasKey(pa => pa.Id);

            // Properties
            builder.Property(pa => pa.DisplayOrder)
                   .HasDefaultValue(0);

            builder.Property(pa => pa.IsRequired)
                   .HasDefaultValue(false);

            builder.Property(pa => pa.IsActive)
                   .HasDefaultValue(true);

            builder.Property(pa => pa.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(pa => pa.ProductId);

            builder.HasIndex(pa => pa.AddonId);

            // Prevent duplicate addon for the same product
            builder.HasIndex(pa => new
            {
                pa.ProductId,
                pa.AddonId
            }).IsUnique();

            // Relationships
            builder.HasOne(pa => pa.Product)
                   .WithMany(p => p.ProductAddons)
                   .HasForeignKey(pa => pa.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pa => pa.Addon)
                   .WithMany(a => a.ProductAddons)
                   .HasForeignKey(pa => pa.AddonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
