using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Onboarding.Domain;
using EquillibriumERP.Core.Onboarding.Persistence;

namespace EquillibriumERP.Core.Onboarding.Services;
public sealed class TenantOnboardingService : ITenantOnboardingService
{
    private readonly OnboardingDbContext _db;
    private readonly ITenantProvisioningService _provisioning;
    //private readonly ITenantAdminUserService _adminUserService;
    private readonly IEnumerable<ITenantModuleSeeder> _seeders;
    private readonly IRawMaterialSeeder _rawMaterialSeeders;

    public TenantOnboardingService(
    OnboardingDbContext db,
    ITenantProvisioningService provisioning,
    IEnumerable<ITenantModuleSeeder> seeders,IRawMaterialSeeder rawMaterialSeeders)
    {
        _db = db;
        _provisioning = provisioning;
        _seeders = seeders;
        _rawMaterialSeeders = rawMaterialSeeders;
    }

    public async Task<Guid> OnboardTenantAsync(
    string tenantName,
    CancellationToken ct)
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
        await _db.SaveChangesAsync(ct);

        // 1. CREATE SCHEMA (NO SESSION)
        await _db.Database.ExecuteSqlRawAsync($@"
            CREATE SCHEMA IF NOT EXISTS {schema};
        ", ct);

        // 2. PROVISION + MIGRATE (NO SESSION)
        await _provisioning.CreateTenantSchemaAsync(
            tenantId,
            schema,
            ct);


        await _rawMaterialSeeders.SeedAsync(schema,ct);
        return tenantId;
    }
}