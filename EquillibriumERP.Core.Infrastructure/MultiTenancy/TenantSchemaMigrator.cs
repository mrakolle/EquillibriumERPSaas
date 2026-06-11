using System.Data;
using EquillibriumERP.Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Core.Abstractions.Modules;

namespace EquillibriumERP.Core.Infrastructure.MultiTenancy;

public class TenantSchemaMigrator
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEnumerable<IModule> _modules;

    public TenantSchemaMigrator(
        IServiceProvider serviceProvider,
        IEnumerable<IModule> modules)
    {
        _serviceProvider = serviceProvider;
        _modules = modules;
    }

    public async Task MigrateAsync(string schema, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        foreach (var module in _modules
            .Where(m => m.Name != "Onboarding")
            .OrderBy(m => m.Name))
        {
            cancellationToken.ThrowIfCancellationRequested();

            Console.WriteLine($"➡ Migrating module: {module.Name}");

            await module.MigrateAsync(scope.ServiceProvider, schema, cancellationToken);
        }

        Console.WriteLine($"✅ Tenant schema fully migrated: {schema}");
    }
}


/* Old class
public class TenantSchemaMigrator
{
    private readonly IServiceProvider _serviceProvider;

    public TenantSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync(string schema)
    {
        using var scope = _serviceProvider.CreateScope();

        // Resolve DbContext
        var dbContext =
            scope.ServiceProvider.GetRequiredService<TenantDbContext>();

        // Ensure connection open
        var connection = dbContext.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        // 🔥 CRITICAL FIX
        // Force PostgreSQL to use tenant schema FIRST
        await dbContext.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public");

        Console.WriteLine($"✅ search_path set to: {schema}, public");

        // 🔥 Apply migrations INTO tenant schema
        await dbContext.Database.MigrateAsync();

        Console.WriteLine($"✅ Tenant schema migrated: {schema}");
    }
}*/