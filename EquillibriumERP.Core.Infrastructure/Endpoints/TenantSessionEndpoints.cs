using EquillibriumERP.Core.Abstractions.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EquillibriumERP.Core.Infrastructure.Endpoints;

public static class TenantSessionEndpoints
{
    public static void MapTenantSessionEndpoints(this WebApplication app)
    {
        app.MapPost("/session/tenant/{tenantId:guid}",
        (Guid tenantId, [FromServices] ITenantSession tenantSession) =>
        {
            tenantSession.TenantId = tenantId;

            return Results.Ok(new
            {
                Message = "Tenant session updated",
                TenantId = tenantId
            });
        });

        app.MapGet("/session/tenant",
        ([FromServices] ITenantSession tenantSession) =>
        {
            return Results.Ok(new
            {
                tenantSession.TenantId
            });
        });
    }
}