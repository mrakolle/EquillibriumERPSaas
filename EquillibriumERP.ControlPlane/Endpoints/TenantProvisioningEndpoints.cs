using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.MultiTenancy;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.ControlPlane.Onboarding;
using Microsoft.AspNetCore.Mvc;

namespace EquillibriumERP.ControlPlane.Endpoints;

public static class TenantProvisioningEndpoints
{
    public static void MapTenantProvisioningEndpoints(this WebApplication app)
{
    app.MapGet("/secure-test", () => "OK")
       .RequireAuthorization();

    app.MapPost("/tenants/onboard", async (
        TenantOnboardingRequest request,
        ITenantOnboardingService service,
        CancellationToken ct) =>
    {
        var id = await service.OnboardTenantAsync(request.TenantName, ct);
        return Results.Ok(new { TenantId = id });
    });
}

public record TenantOnboardingRequest(string TenantName);

      /*  Console.WriteLine("CONTROL PLANE ONBOARD HIT 4");
        app.MapPost("/tenants/onboard", async (
            MasterDbContext db,
            TenantProvisioningService provisioning,
            [FromBody] CreateTenantRequest request) =>
        {
            var tenantId = Guid.NewGuid();
            var schema = $"tenant_{tenantId:N}";

            // ensure schema + migrations first
            await provisioning.CreateTenantSchemaAsync(tenantId);

            // now persist tenant INSIDE tenant context
            var tenant = new Tenant
            {
                Id = tenantId,
                Name = request.TenantName,
                Schema = schema,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            db.Set<Tenant>().Add(tenant);

            await db.SaveChangesAsync();

            return Results.Ok(new
            {
                tenant.Id,
                tenant.Name,
                tenant.Schema,
                Message = "Tenant onboarded via TenantDbContext"
            });
        }); */
}
