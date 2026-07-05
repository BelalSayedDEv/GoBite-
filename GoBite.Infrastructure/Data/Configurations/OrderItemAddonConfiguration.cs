using GoBite.Domain.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    internal class OrderItemAddonConfiguration : IEntityTypeConfiguration<OrderItemAddon>
    {
        public void Configure(EntityTypeBuilder<OrderItemAddon> builder)
        {
            // Table
            builder.ToTable("OrderItemAddons");

            // Primary Key
            builder.HasKey(oia => oia.Id);

            // Properties
            builder.Property(oia => oia.AddonName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(oia => oia.UnitPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(oia => oia.Quantity)
                   .IsRequired()
                   .HasDefaultValue(1);

            builder.Property(oia => oia.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(oia => oia.OrderItemId);

            builder.HasIndex(oia => oia.AddonId);

            // Prevent duplicate addon per order item
            builder.HasIndex(oia => new
            {
                oia.OrderItemId,
                oia.AddonId
            }).IsUnique();

            // Relationships
            builder.HasOne(oia => oia.OrderItem)
                   .WithMany(oi => oi.OrderItemAddons)
                   .HasForeignKey(oia => oia.OrderItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oia => oia.Addon)
                   .WithMany(a => a.OrderItemAddons)
                   .HasForeignKey(oia => oia.AddonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
