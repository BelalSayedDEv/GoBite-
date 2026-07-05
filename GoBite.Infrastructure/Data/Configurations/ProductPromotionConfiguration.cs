using GoBite.Domain.PromotionEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class ProductPromotionConfiguration : IEntityTypeConfiguration<ProductPromotion>
    {
        public void Configure(EntityTypeBuilder<ProductPromotion> builder)
        {
            // Table
            builder.ToTable("ProductPromotions");

            // Primary Key
            builder.HasKey(pp => pp.Id);

            // Indexes
            builder.HasIndex(pp => pp.ProductId);

            builder.HasIndex(pp => pp.PromotionId);

            // prevent 
            builder.HasIndex(pp => new
            {
                pp.ProductId,
                pp.PromotionId
            }).IsUnique();

            // Relationships
            builder.HasOne(pp => pp.Product)
                   .WithMany(p => p.ProductPromotions)
                   .HasForeignKey(pp => pp.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pp => pp.Promotion)
                   .WithMany(p => p.ProductPromotions)
                   .HasForeignKey(pp => pp.PromotionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
