using EquillibriumERP.Abstractions.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EquillibriumERP.Infrastructure.Runtime;

public static class TenantRuntimeEndpoints
{
    public static void MapTenantRuntime(this WebApplication app)
    {
        app.MapPost("/runtime/tenant/switch/{tenantId:guid}",
        (Guid tenantId, ITenantSession session) =>
        {
            session.TenantId = tenantId;

            return Results.Ok(new
            {
                message = "Tenant switched for current session",
                tenantId
            });
        });
    }
}