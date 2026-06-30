using Microsoft.EntityFrameworkCore;
using EquillibriumERP.ControlPlane.Interfaces;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.ControlPlane.Infrastructure.Persistence;

namespace EquillibriumERP.ControlPlane.Services;
public class TenantMigrationService : ITenantMigrationService
{
    private readonly ITenantProvisioningService _provisioning;
    private readonly ControlPlaneDbContext _db;
    //private readonly IMasterDbContext _masterdb;
    private readonly IEnumerable<ITenantModuleSeeder> _seeders;
    private readonly IRawMaterialSeeder _rawMaterialSeeders;

    public TenantMigrationService(
    ControlPlaneDbContext db,
    ITenantProvisioningService provisioning,
    IEnumerable<ITenantModuleSeeder> seeders,IRawMaterialSeeder rawMaterialSeeders)
    {
        _db = db;
       // _masterdb = masterdb;
        _provisioning = provisioning;
        _seeders = seeders;
        _rawMaterialSeeders = rawMaterialSeeders;
    }

    public async Task UpdateTenantSchemasAsync(CancellationToken ct)
    {
        var tenants = await _db.GetActiveTenantsAsync(ct);

        foreach (var tenant in tenants)
        {
            Console.WriteLine("Updating Tenant " + tenant.Schema + "'s Schema");
            await _provisioning.UpdateTenantSchemaAsync(
                tenant.Schema,
                ct);
        }
    }
}