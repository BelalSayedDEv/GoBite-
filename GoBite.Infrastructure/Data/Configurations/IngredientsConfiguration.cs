using GoBite.Domain.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class IngredientsConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            // Table
            builder.ToTable("Ingredients");

            // Primary Key
            builder.HasKey(i => i.Id);

            // Properties
            builder.Property(i => i.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(i => i.Description)
                   .HasMaxLength(1000);

            builder.Property(i => i.DisplayOrder)
                   .HasDefaultValue(0);

            builder.Property(i => i.IsActive)
                   .HasDefaultValue(true);

            builder.Property(i => i.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(i => i.Name);

            builder.HasIndex(i => i.DisplayOrder);
        }
    }
}
