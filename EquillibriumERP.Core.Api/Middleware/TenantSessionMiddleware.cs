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
        // ==========================================================
        // STEP 1: READ TENANT HEADER
        // ==========================================================
        var tenantHeader =
            context.Request.Headers["X-Tenant-Id"]
            .FirstOrDefault();

        Console.WriteLine("========== MIDDLEWARE START ==========");
        Console.WriteLine($"[MIDDLEWARE] Tenant header = {tenantHeader}");

        if (Guid.TryParse(tenantHeader, out var tenantId))
        {
            // ==========================================================
            // STEP 2: RESOLVE SCHEMA
            // ==========================================================
            tenantResolver.SetTenant(tenantId.ToString());

            var schema = tenantResolver.GetSchema();

            // ==========================================================
            // STEP 3: SET SESSION
            // ==========================================================
            tenantSession.SetTenant(tenantId, schema);

            // ==========================================================
            // DEBUG OUTPUT (CRITICAL)
            // ==========================================================
            Console.WriteLine($"[MIDDLEWARE] SESSION TENANT = {tenantId}");
            Console.WriteLine($"[MIDDLEWARE] SCHEMA = {schema}");
            Console.WriteLine($"[MIDDLEWARE] SESSION REF CHECK:");
            Console.WriteLine($"    TenantId  => {tenantSession.TenantId}");
            Console.WriteLine($"    Schema    => {tenantSession.Schema}");

            Console.WriteLine("========== MIDDLEWARE END ==========");
        }
        else
        {
            Console.WriteLine("NO TENANT HEADER");
        }

        await _next(context);
    }
}