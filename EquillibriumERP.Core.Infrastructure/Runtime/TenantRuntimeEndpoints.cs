using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Infrastructure.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EquillibriumERP.Core.Infrastructure.Runtime;

public static class TenantRuntimeEndpoints
{
    public static void MapTenantRuntime(this WebApplication app)
    {
        app.MapGet("/secure-test", () => "OK")
        .RequireAuthorization();

        app.MapPost("/runtime/tenant/switch/{tenantId:guid}", (
            Guid tenantId,
            ITenantSession session,
            ITenantResolver resolver) =>
        {
            session.TenantId = tenantId;
            resolver.SetTenant(tenantId.ToString());

            return Results.Ok(new
            {
                message = "Tenant switched for current session",
                tenantId,
                schema = resolver.GetSchema()
            });
        });
    }
}