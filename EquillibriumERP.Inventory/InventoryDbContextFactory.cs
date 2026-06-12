using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Inventory;

public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
{
    public InventoryDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var options = new DbContextOptionsBuilder<InventoryDbContext>()
            .UseNpgsql(config.GetConnectionString("TenantDatabase"))
            .Options;

        var services = new ServiceCollection();

        services.AddSingleton<ITenantSession, DesignTimeTenantSession>();

        var provider = services.BuildServiceProvider();
        var tenantSession = provider.GetRequiredService<ITenantSession>();

        tenantSession.TenantId = Guid.Empty;

        return new InventoryDbContext(options, tenantSession);
    }
}