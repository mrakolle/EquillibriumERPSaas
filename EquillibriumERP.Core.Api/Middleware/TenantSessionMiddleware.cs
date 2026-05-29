using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Api.Middleware;

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
        ITenantSession tenantSession,
        ITenantResolver tenantResolver)
    {
        var tenantHeader =
            context.Request.Headers["X-Tenant-Id"]
            .FirstOrDefault();

        if (Guid.TryParse(tenantHeader, out var tenantId))
        {
            tenantSession.TenantId = tenantId;

            // restore resolver propagation
            tenantResolver.SetTenant(
                tenantId.ToString());

            Console.WriteLine(
                $"SESSION TENANT = {tenantId}");

            Console.WriteLine(
                $"SCHEMA = {tenantResolver.GetSchema()}");
        }
        else
        {
            Console.WriteLine("NO TENANT HEADER");
        }

        await _next(context);
    }
}

/*
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Api.Middleware;

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
*/