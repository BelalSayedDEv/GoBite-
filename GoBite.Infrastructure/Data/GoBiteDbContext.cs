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


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AddonConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(IngredientsConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(InventoryConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(AddonConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(OtpCodeConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(ProductAddonConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(ProductIngredientCofiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(RefreshTokenConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(CartItemConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(CartItemAddonConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(CartItemInfrediantsRemovedConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(OrderItemConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(OrderItemIngredientConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(OrderItemAddonConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(NotificationConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(CommentConfiguration).Assembly);
    }
}
