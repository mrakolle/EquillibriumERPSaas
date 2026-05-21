using EquillibriumERP.Abstractions.MultiTenancy;

namespace EquillibriumERP.Api.Middleware;

public class TenantSessionMiddleware
{
    private readonly RequestDelegate _next;

    public TenantSessionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ITenantSession tenantSession)
    {
        var tenantHeader =
            context.Request.Headers["X-Tenant-Id"]
            .FirstOrDefault();

        if (Guid.TryParse(tenantHeader, out var tenantId))
        {
            tenantSession.TenantId = tenantId;

            Console.WriteLine($"SESSION TENANT = {tenantId}");
        }
        else
        {
            Console.WriteLine("NO TENANT HEADER");
        }

        await _next(context);
    }
}