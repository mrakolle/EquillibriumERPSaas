using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.ControlPlane.Interfaces;

namespace EquillibriumERP.ControlPlane.Endpoints;

public static class TenantAdminEndpoints
{
    public static void MapTenantAdminEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/system/admin");

        MapUpdateTenantSchemas(group);
    }

    private static void MapUpdateTenantSchemas(RouteGroupBuilder group)
    {
        group.MapPost("/updateSchemas", async (
            ITenantMigrationService migrationService,
            CancellationToken ct) =>
        {
            Console.WriteLine("Updating Schemas...");
            await migrationService.UpdateTenantSchemasAsync(ct);
            return Results.Ok("Tenant schemas updated successfully.");
        });
    }
}

