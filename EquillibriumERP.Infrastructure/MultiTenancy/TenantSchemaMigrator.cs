using System.Data;
using EquillibriumERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Infrastructure.MultiTenancy;

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
}