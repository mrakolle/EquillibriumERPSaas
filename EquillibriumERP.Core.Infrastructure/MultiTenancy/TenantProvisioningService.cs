using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Infrastructure.Persistence;

namespace EquillibriumERP.Core.Infrastructure.MultiTenancy;

public class TenantProvisioningService
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
}