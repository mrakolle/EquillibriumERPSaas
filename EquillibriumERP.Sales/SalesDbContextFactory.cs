using System;
using System.IO;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Sales;

public class SalesDbContextFactory : IDesignTimeDbContextFactory<SalesDbContext>
{
    public SalesDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var options = new DbContextOptionsBuilder<SalesDbContext>()
            .UseNpgsql(config.GetConnectionString("TenantDatabase"))
            .Options;

        var services = new ServiceCollection();

        services.AddSingleton<ITenantSession, DesignTimeTenantSession>();
        services.AddSingleton<ITenantResolver, DesignTimeTenantResolver>();

        var provider = services.BuildServiceProvider();

        var tenantSession = provider.GetRequiredService<ITenantSession>();
        tenantSession.TenantId = Guid.Empty;

        return new SalesDbContext(options, tenantSession);
    }
}