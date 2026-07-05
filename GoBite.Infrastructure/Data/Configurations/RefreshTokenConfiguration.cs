using GoBite.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoBite.Infrastructure.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Token).HasMaxLength(256).IsRequired();
        builder.Property(rt => rt.JwtId).HasMaxLength(128).IsRequired();
        builder.Property(rt => rt.UserId).IsRequired();
        builder.HasIndex(rt => rt.Token);
        builder.HasOne(rt => rt.User)
              .WithMany()
              .HasForeignKey(rt => rt.UserId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
