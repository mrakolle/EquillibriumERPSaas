using EquillibriumERP.Infrastructure.Persistence;
using EquillibriumERP.Infrastructure.Persistence.Entities;
using EquillibriumERP.Infrastructure.MultiTenancy;
using Microsoft.AspNetCore.Builder;

namespace EquillibriumERP.Api.Endpoints;

public static class TenantOnboardingEndpoints
{
    public static IEndpointRouteBuilder MapTenantOnboardingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/tenants/onboard", async (
            MasterDbContext masterDb,
            TenantProvisioningService provisioning,
            string tenantName) =>
        {
            // 1. Generate tenant id
            var tenantId = Guid.NewGuid();

            // 2. Schema is derived ONLY for storage (not passed around as source of truth)
            var schemaName = $"tenant_{tenantId:N}";

            // 3. Save tenant in master DB
            var tenant = new Tenant
            {
                Id = tenantId,
                Name = tenantName,
                Schema = schemaName,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            masterDb.Tenants.Add(tenant);
            await masterDb.SaveChangesAsync();

            // 4. FIX: pass Guid (not string)
            await provisioning.CreateTenantSchemaAsync(tenantId);

            return Results.Ok(new
            {
                tenant.Id,
                tenant.Name,
                tenant.Schema,
                Message = "Tenant created + schema provisioned"
            });
        });

        return app;
    }
}