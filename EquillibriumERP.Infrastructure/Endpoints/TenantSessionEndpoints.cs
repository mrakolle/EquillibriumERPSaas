using EquillibriumERP.Abstractions.MultiTenancy;
using EquillibriumERP.Infrastructure.Persistence;
using EquillibriumERP.Infrastructure.MultiTenancy;
using EquillibriumERP.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquillibriumERP.Infrastructure.Endpoints;

public static class TenantSessionEndpoints
{
    public static void MapTenantSessionEndpoints(
        this WebApplication app)
    {
        app.MapPost("/session/tenant/{tenantId:guid}",
        (
            Guid tenantId,
            ITenantSession tenantSession) =>
        {
            tenantSession.TenantId = tenantId;

            return Results.Ok(new
            {
                Message = "Tenant session updated",
                TenantId = tenantId
            });
        });

        app.MapGet("/session/tenant",
            (ITenantSession tenantSession) =>
        {
            return Results.Ok(new
            {
                tenantSession.TenantId
            });
        });
        app.MapPost("/tenants/onboard", async (
        [FromServices] MasterDbContext masterDb,
        [FromServices] TenantProvisioningService provisioning,
        CreateTenantRequest request) =>
        {
            // 1. Generate tenant id
            var tenantId = Guid.NewGuid();

            // 2. Schema (derived, not trusted input)
            var schemaName = $"tenant_{tenantId:N}";

            // 3. Save tenant in master DB
            var tenant = new Tenant
            {
                Id = tenantId,
                Name = request.TenantName,
                Schema = schemaName,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            masterDb.Tenants.Add(tenant);
            await masterDb.SaveChangesAsync();

            // 4. Provision schema
            await provisioning.CreateTenantSchemaAsync(tenantId);

            return Results.Ok(new
            {
                tenant.Id,
                tenant.Name,
                tenant.Schema,
                Message = "Tenant created + schema provisioned"
            });
        });
    }
}
