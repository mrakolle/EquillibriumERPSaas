using System;
using System.Threading;
using System.Threading.Tasks;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Onboarding.Domain;
using EquillibriumERP.Core.Onboarding.Persistence;
using EquillibriumERP.Core.Abstractions; 


namespace EquillibriumERP.Core.Onboarding.Services;

public sealed class TenantOnboardingService : ITenantOnboardingService
{
    private readonly OnboardingDbContext _db;
    private readonly ITenantProvisioningService _provisioning;

    public TenantOnboardingService(
        OnboardingDbContext db,
        ITenantProvisioningService provisioning)
    {
        _db = db;
        _provisioning = provisioning;
    }

   public async Task<Guid> OnboardTenantAsync(string tenantName, CancellationToken cancellationToken)
    {
        var tenantId = Guid.NewGuid();

        var schema = $"tenant_{tenantId:N}";

        var tenant = new Tenant
        {
            Id = tenantId,
            Name = tenantName,
            Code = TenantCodeGenerator.Generate(tenantName),
            Schema = schema,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _db.Set<Tenant>().Add(tenant);

        await _db.SaveChangesAsync(cancellationToken);

        await _provisioning.CreateTenantSchemaAsync(
            tenantId,
            schema,
            cancellationToken);

        return tenantId;
    }
}