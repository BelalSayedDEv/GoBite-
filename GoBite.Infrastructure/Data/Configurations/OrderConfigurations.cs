using GoBite.Domain.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Subtotal)
               .HasPrecision(18, 2);

        builder.Property(o => o.DeliveryFees)
               .HasPrecision(18, 2);

        builder.Property(o => o.TotalPrice)
               .HasPrecision(18, 2);

        builder.Property(o => o.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(o => o.UpdatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(o => o.Customer)
               .WithMany()
               .HasForeignKey(o => o.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.OrderStatus)
               .WithMany(s => s.orders)
               .HasForeignKey(o => o.OrderStatusId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(o => o.OrderItems)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}