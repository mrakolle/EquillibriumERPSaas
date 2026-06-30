using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Manufacturing.Infrastructure.Persistence;

public class ManufacturingDbContextFactory
    : IDesignTimeDbContextFactory<ManufacturingDbContext>
{
    public ManufacturingDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var options = new DbContextOptionsBuilder<ManufacturingDbContext>()
            .UseNpgsql(
                config.GetConnectionString("TenantDatabase"))
            .Options;

        var services = new ServiceCollection();

        services.AddSingleton<ITenantSession, DesignTimeTenantSession>();
        services.AddSingleton<ITenantResolver, DesignTimeTenantResolver>();

        var provider = services.BuildServiceProvider();

        var tenantSession =
            provider.GetRequiredService<ITenantSession>();

        return new ManufacturingDbContext(
            options,
            tenantSession);
    }
}