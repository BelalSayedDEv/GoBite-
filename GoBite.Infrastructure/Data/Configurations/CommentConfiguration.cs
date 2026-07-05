using GoBite.Domain.CommentEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            // Table
            builder.ToTable("Comments");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");


            builder.Property(c => c.UserId)
                   .IsRequired();

            // Indexes
            builder.HasIndex(c => c.UserId);

            builder.HasIndex(c => c.ProductId);

            builder.HasIndex(c => c.CreatedAt);

            // Relationships
            builder.HasOne(c => c.User)
                   .WithMany(u => u.Comments)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Product)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(c => c.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
