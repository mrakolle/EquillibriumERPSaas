using EquillibriumERP.Core.Infrastructure.MultiTenancy;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;

namespace EquillibriumERP.ControlPlane.Onboarding;

public sealed class TenantOnboardingService : ITenantOnboardingService
{
    private readonly TenantProvisioningService _provisioning;
    private readonly MasterDbContext _db;

    public TenantOnboardingService(
        TenantProvisioningService provisioning,
        MasterDbContext db)
    {
        _provisioning = provisioning;
        _db = db;
    }

    public async Task<Guid> OnboardTenantAsync(
        string tenantName,
        CancellationToken cancellationToken = default)
    {
        var tenantId = Guid.NewGuid();
        var schema = $"tenant_{tenantId:N}";

        await _provisioning.CreateTenantSchemaAsync(tenantId);

        var tenant = new Tenant
        {
            Id = tenantId,
            Name = tenantName,
            Schema = schema,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _db.Set<Tenant>().Add(tenant);
        await _db.SaveChangesAsync(cancellationToken);
       
        return tenantId;
    }
}



/*using EquillibriumERP.Core.Infrastructure.MultiTenancy;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;

namespace EquillibriumERP.ControlPlane.Onboarding;

public sealed class TenantOnboardingService
    : ITenantOnboardingService
{
    private readonly TenantProvisioningService _provisioning;
    private readonly MasterDbContext _masterDb;

    public TenantOnboardingService(
        TenantProvisioningService provisioning,
        MasterDbContext masterDb)
    {
        _provisioning = provisioning;
        _masterDb = masterDb;
    }

    public async Task<Guid> OnboardTenantAsync(
        string tenantName,
        CancellationToken cancellationToken = default)
    {
        var tenantId = Guid.NewGuid();

        // 1. Create schema + run migrations
        var schema = await _provisioning
            .CreateTenantSchemaAsync(tenantId);

        // 2. Register tenant in master DB
        var tenant = new Tenant
        {
            Id = tenantId,
            Name = tenantName,
            Schema = schema,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        Console.WriteLine("ADDING TENANT TO MASTER DB");

        _masterDb.Tenants.Add(tenant);

        Console.WriteLine("SAVING MASTER DB");

        await _masterDb.SaveChangesAsync(cancellationToken);

        Console.WriteLine("MASTER DB SAVE COMPLETE");

        return tenantId;
    }
}*/

   /* public async Task<Guid> OnboardTenantAsync(
        string tenantName,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine("CONTROL PLANE ONBOARD HIT");
        var tenantId = Guid.NewGuid();

        // TODO:
        // 1. Create tenant record
        // 2. Provision schema
        // 3. Activate default modules
        // 4. Run migrations
        // 5. Seed defaults

        await Task.CompletedTask;

        return tenantId;
    }*/
