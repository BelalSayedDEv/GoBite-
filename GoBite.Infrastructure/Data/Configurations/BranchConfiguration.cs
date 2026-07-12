using GoBite.Domain.Branch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                   .HasMaxLength(200);

            builder.Property(b => b.Address)
                   .HasMaxLength(500);

            builder.Property(b => b.Phone)
                   .HasMaxLength(20);

            builder.Property(b => b.Latitude)
                   .HasPrecision(9, 6);

            builder.Property(b => b.Longitude)
                   .HasPrecision(9, 6);

            builder.Property(b => b.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
