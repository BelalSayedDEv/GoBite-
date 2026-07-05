using GoBite.Domain.CartEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            // Table
            builder.ToTable("CartItems");

            // Primary Key
            builder.HasKey(ci => ci.Id);

            // Properties
            builder.Property(ci => ci.Quantity)
                   .IsRequired()
                   .HasDefaultValue(1);

            builder.Property(ci => ci.SpecialInstructions)
                   .HasMaxLength(1000);

            builder.Property(ci => ci.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(ci => ci.CartId);

            builder.HasIndex(ci => ci.ProductId);


            builder.HasIndex(ci => new
            {
                ci.CartId,
                ci.ProductId
            }).IsUnique();

            // Relationships
            builder.HasOne(ci => ci.Cart)
                   .WithMany(c => c.CartItems)
                   .HasForeignKey(ci => ci.CartId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.Product)
                   .WithMany(p => p.CartItems)
                   .HasForeignKey(ci => ci.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
