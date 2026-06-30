using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Api.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(
    HttpContext context,
    ITenantSession tenantSession,
    ITenantResolver tenantResolver)
    {
        var tenantHeader =
            context.Request.Headers["X-Tenant-Id"]
            .FirstOrDefault();

        if (Guid.TryParse(tenantHeader, out var tenantId))
        {
            tenantResolver.SetTenant(tenantId.ToString());

            var schema = tenantResolver.GetSchema();
            //debug start
            Console.WriteLine(
                        $"JWT tenant_id = '{context.User.FindFirst("tenant_id")?.Value}'");

                    Console.WriteLine(
                        $"JWT schema = '{context.User.FindFirst("schema")?.Value}'");
            //debug end
            tenantSession.SetTenant(
                tenantId,
                schema);
            
            Console.WriteLine($"MIDDLEWARE -> TenantId={tenantId}, Schema={schema}");
        }
        else if (context.User.Identity?.IsAuthenticated == true)
        {
            var tenantIdClaim =
                context.User.FindFirst("tenant_id")?.Value;

            var schema =
                context.User.FindFirst("schema")?.Value;

            if (Guid.TryParse(
                    tenantIdClaim,
                    out tenantId)
                && !string.IsNullOrWhiteSpace(schema))
            {
                tenantSession.SetTenant(
                    tenantId,
                    schema);
            }
        }
        await _next(context);
    }
}
    // commented on 17 June 2026 to introduce InvokeAsync above
    /*public async Task InvokeAsync(
    HttpContext context,
    ITenantSession tenantSession,
    ITenantResolver tenantResolver)
    {
        var tenantHeader =
            context.Request.Headers["X-Tenant-Id"]
            .FirstOrDefault();

        if (Guid.TryParse(tenantHeader, out var tenantId))
        {
            tenantResolver.SetTenant(tenantId.ToString());

            var schema = tenantResolver.GetSchema();

            tenantSession.SetTenant(tenantId, schema);
        }

        await _next(context);
    }
}*

// This was for test purposes only
/*using System.Security.Claims;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Api.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ITenantSession tenantSession,
        ITenantResolver tenantResolver)
    {
        // =========================================
        // JWT TENANT RESOLUTION
        // =========================================

        var tenantClaim = context.User.FindFirst("tenant_id")?.Value;

        if (Guid.TryParse(tenantClaim, out var tenantId))
        {
            // request/session state
            tenantSession.TenantId = tenantId;

            // schema resolver state
            tenantResolver.SetTenant(tenantId.ToString());

            Console.WriteLine($"JWT TENANT ACTIVE = {tenantId}");
        }

        await _next(context);
    }
}*/




// end of test only

    /*public async Task InvokeAsync(
    HttpContext context,
    ITenantSession tenantSession,
    ITenantResolver tenantResolver)
    {
        var tenantHeader = 
            context.Request.Headers["X-Tenant-Id"].FirstOrDefault();

        if (Guid.TryParse(tenantHeader, out var tenantId))
        {
            tenantSession.TenantId = tenantId;

            // 🔥 IMPORTANT BRIDGE (keeps old system working)
            tenantResolver.SetTenant(tenantId.ToString());
        }

        await _next(context);
    }*/
