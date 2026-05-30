using EquillibriumERP.Core.Infrastructure.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EquillibriumERP.ControlPlane.Endpoints;

public static class PermissionEndpoints
{
    public static void MapPermissionEndpoints(
        WebApplication app)
    {
        app.MapGet(
            "/api/controlplane/permissions",
            (IServiceProvider sp) =>
            {
                var permissions =
                    PermissionRegistry.GetAll(sp);

                return Results.Ok(permissions);
            });
    }
}