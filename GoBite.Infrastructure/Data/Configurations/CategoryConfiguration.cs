using GoBite.Domain.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Table
            builder.ToTable("Categories");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.ImageUrl)
                   .HasMaxLength(500);

            builder.Property(c => c.DisplayOrder)
                   .HasDefaultValue(0);

            builder.Property(c => c.IsActive)
                   .HasDefaultValue(true);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(c => c.Name);

            builder.HasIndex(c => c.DisplayOrder);
        }
    }
}
