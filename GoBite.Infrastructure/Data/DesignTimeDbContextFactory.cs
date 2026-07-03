using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GoBite.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GoBiteDbContext>
{
    public GoBiteDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../GoBite.API"))
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<GoBiteDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseSqlServer(connectionString,
            b => b.MigrationsAssembly("GoBite.Infrastructure"));

        return new GoBiteDbContext(builder.Options);
    }
}
