using System;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Infrastructure.MultiTenancy;

public class TenantProvisioningService : ITenantProvisioningService
{
   private readonly MasterDbContext _masterDb;
    private readonly TenantSchemaMigrator _migrator;

    public TenantProvisioningService(
        MasterDbContext masterDb,
        TenantSchemaMigrator migrator)
    {
        _masterDb = masterDb;
        _migrator = migrator;
    }
    

   public async Task<string> CreateTenantSchemaAsync(
    Guid tenantId,
    string schema,
    CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(schema))
            throw new ArgumentException("Schema cannot be empty");

        // 1. CREATE SCHEMA (real DB object)
        var sql = $"CREATE SCHEMA IF NOT EXISTS \"{schema}\";";

        await _masterDb.Database.ExecuteSqlRawAsync(
            sql,
            cancellationToken);

        // 2. RUN MODULE MIGRATIONS
        await _migrator.MigrateAsync(schema, cancellationToken);
       
        // 3. SEED ADMIN USER
        Console.WriteLine("This is where Admin User for SCHEMA: " + schema + " must be created");

        return schema;
    }

    public async Task UpdateTenantSchemaAsync(
    string schema,
    CancellationToken cancellationToken)
    {
        Console.WriteLine("Updating " + schema + " Schema");
        await _migrator.MigrateAsync(schema, cancellationToken);
    }
}

/*public class TenantProvisioningService
{
    private readonly MasterDbContext _masterDb;
    private readonly TenantSchemaMigrator _migrator;

    public TenantProvisioningService(
        MasterDbContext masterDb,
        TenantSchemaMigrator migrator)
    {
        _masterDb = masterDb;
        _migrator = migrator;
    }

    public async Task<string> CreateTenantSchemaAsync(Guid tenantId)
    {
        var schema = $"tenant_{tenantId:N}";

        // 1. Ensure schema exists (idempotent, safe)
        await _masterDb.Database.ExecuteSqlRawAsync(
            $"""CREATE SCHEMA IF NOT EXISTS "{schema}";""");

        // 2. Apply migrations for this tenant schema
        await _migrator.MigrateAsync(schema);

        return schema;
    }
}*/