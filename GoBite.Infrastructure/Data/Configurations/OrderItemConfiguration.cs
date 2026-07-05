using GoBite.Domain.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Table
            builder.ToTable("OrderItems");

            // Primary Key
            builder.HasKey(oi => oi.Id);

            // Properties
            builder.Property(oi => oi.ProductName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(oi => oi.UnitPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.Quantity)
                   .IsRequired();

            builder.Property(oi => oi.SpecialInstructions)
                   .HasMaxLength(1000);

            builder.Property(oi => oi.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(oi => oi.OrderId);

            builder.HasIndex(oi => oi.ProductId);

            // Relationships
            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                   .WithMany(p => p.OrderItems)
                   .HasForeignKey(oi => oi.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

