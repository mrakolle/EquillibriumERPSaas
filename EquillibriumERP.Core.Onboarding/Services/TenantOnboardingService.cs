using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Onboarding.Domain;
using EquillibriumERP.Core.Onboarding.Contracts;
using EquillibriumERP.Core.Onboarding.Persistence;

namespace EquillibriumERP.Core.Onboarding.Services;
public sealed class TenantOnboardingService : ITenantOnboardingService
{
    private readonly OnboardingDbContext _db;
    private readonly ITenantProvisioningService _provisioning;
    //private readonly ITenantAdminUserService _adminUserService;
    private readonly IEnumerable<ITenantModuleSeeder> _seeders;

    public TenantOnboardingService(
        OnboardingDbContext db,
        ITenantProvisioningService provisioning,
        IEnumerable<ITenantModuleSeeder> seeders)
    {
        _db = db;
        _provisioning = provisioning;
        _seeders = seeders;
    }

    public async Task<TenantOnboardingResult> OnboardTenantAsync(
    CreateTenantRequest request,
    CancellationToken ct)
    {
        var tenantId = Guid.NewGuid();

        var schema = $"tenant_{tenantId:N}";

        var tenant = new Tenant
        {
            Id = tenantId,

            Name = request.Company.CompanyName,

            Code = TenantCodeGenerator.Generate(
                request.Company.CompanyName),

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


        foreach (var seeder in _seeders.OrderBy(s => s.Order))
        {
            await seeder.SeedAsync(schema, ct);
        }
        return new TenantOnboardingResult(
            tenant.Id,
            tenant.Code,
            tenant.Schema

        );
    }
}