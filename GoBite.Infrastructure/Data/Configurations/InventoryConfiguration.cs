using GoBite.Domain.Branch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<BranchInventory>
    {
        public void Configure(EntityTypeBuilder<BranchInventory> builder)
        {
            // Table
            builder.ToTable("Inventories");

            // Primary Key
            builder.HasKey(i => i.Id);

            // Properties
            builder.Property(i => i.CurrentQuantity)
                   .HasColumnType("decimal(18,2)");

            builder.Property(i => i.MinimumQuantity)
                   .HasColumnType("decimal(18,2)");

            builder.Property(i => i.UpdatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(i => i.BranchId);

            builder.HasIndex(i => i.IngredientId);

            builder.HasIndex(i => new
            {
                i.BranchId,
                i.IngredientId
            }).IsUnique();

            // Relationships
            builder.HasOne(i => i.Branch)
                   .WithMany(b => b.Inventories)
                   .HasForeignKey(i => i.BranchId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
