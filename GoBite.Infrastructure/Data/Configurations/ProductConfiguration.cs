using GoBite.Domain.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Table
            builder.ToTable("Products");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Description)
                   .HasMaxLength(2000);

            builder.Property(p => p.BasePrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ImageUrl)
                   .HasMaxLength(1000);

            builder.Property(p => p.IsAvailable)
                   .HasDefaultValue(true);

            builder.Property(p => p.IsActive)
                   .HasDefaultValue(true);

            builder.Property(p => p.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.UpdatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(p => p.Name);

            builder.HasIndex(p => p.CategoryId);

            builder.HasIndex(p => new
            {
                p.CategoryId,
                p.IsActive,
                p.IsAvailable
            });

            // Relationships
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
