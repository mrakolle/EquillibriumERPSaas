using System;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Core.Identity.Infrastructure.DesignTime;

public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var options = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(config.GetConnectionString("TenantDatabase"))
            .Options;

        var services = new ServiceCollection();

        services.AddSingleton<ITenantSession, DesignTimeTenantSession>();
        services.AddSingleton<ITenantResolver, DesignTimeTenantResolver>();

        var provider = services.BuildServiceProvider();

        var tenantSession = provider.GetRequiredService<ITenantSession>();

        //tenantSession.TenantId = Guid.Empty;

        return new IdentityDbContext(options, tenantSession);
    }
    public void SetTenant(Guid tenantId, string schema)
    {
        // No-op for design time.
    }

    public void Clear()
    {
        // No-op for design time.
    }
}