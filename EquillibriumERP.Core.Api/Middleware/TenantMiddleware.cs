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
        var tenantClaim = context.User.FindFirst("tenant_id")?.Value;

        if (Guid.TryParse(tenantClaim, out var tenantId))
        {
            tenantSession.TenantId = tenantId;

            tenantResolver.SetTenant(tenantId.ToString());
        }

        await _next(context);
    }
}

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
