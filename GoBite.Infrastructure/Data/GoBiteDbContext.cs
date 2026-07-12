using GoBite.Domain.CartEntities;
using GoBite.Domain.Entities;
using GoBite.Infrastructure.Data.Configurations;
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

    public DbSet<Cart> Carts { get; set; }

    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<CartItemAddon> ItemAddons { get; set; }

    public DbSet<CartItemRemovedIngredient> CartItemRemoveds { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AddonConfiguration).Assembly);
       
    }
}
