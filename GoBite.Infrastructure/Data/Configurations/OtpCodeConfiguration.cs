using GoBite.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations;

public class OtpCodeConfiguration : IEntityTypeConfiguration<OtpCode>
{
    public void Configure(EntityTypeBuilder<OtpCode> builder)
    {
        builder.ToTable("OtpCodes");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Code).HasMaxLength(6).IsRequired();
        builder.Property(o => o.UserId).IsRequired();
        builder.HasIndex(o => new { o.UserId, o.Code });
        builder.HasOne(o => o.User)
              .WithMany()
              .HasForeignKey(o => o.UserId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
