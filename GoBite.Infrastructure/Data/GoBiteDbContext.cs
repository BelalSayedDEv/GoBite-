using GoBite.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoBite.Infrastructure.Data;

public class GoBiteDbContext : IdentityDbContext<ApplicationUser>
{
    public GoBiteDbContext(DbContextOptions<GoBiteDbContext> options) : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<OtpCode> OtpCodes => Set<OtpCode>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshTokens");
            entity.HasKey(rt => rt.Id);
            entity.Property(rt => rt.Token).HasMaxLength(256).IsRequired();
            entity.Property(rt => rt.JwtId).HasMaxLength(128).IsRequired();
            entity.Property(rt => rt.UserId).IsRequired();
            entity.HasIndex(rt => rt.Token);
            entity.HasOne(rt => rt.User)
                  .WithMany()
                  .HasForeignKey(rt => rt.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<OtpCode>(entity =>
        {
            entity.ToTable("OtpCodes");
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Code).HasMaxLength(6).IsRequired();
            entity.Property(o => o.UserId).IsRequired();
            entity.HasIndex(o => new { o.UserId, o.Code });
            entity.HasOne(o => o.User)
                  .WithMany()
                  .HasForeignKey(o => o.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
