using EquillibriumERP.Abstractions.MultiTenancy;

namespace EquillibriumERP.Api.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ITenantResolver tenantResolver)
    {
        // Get tenant from request header (safe extraction)
        var tenantId = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();

        // Fallback behavior preserved + safer
        if (string.IsNullOrWhiteSpace(tenantId))
        {
            tenantId = "default";
        }

        tenantResolver.SetTenant(tenantId);

        await _next(context);
    }
}