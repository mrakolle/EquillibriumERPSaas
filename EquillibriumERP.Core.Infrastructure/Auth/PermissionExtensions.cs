using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EquillibriumERP.Core.Infrastructure.Authorization;

public static class PermissionExtensions
{
    public static RouteHandlerBuilder RequirePermission(
        this RouteHandlerBuilder builder,
        string permission)
    {
        return builder.AddEndpointFilter(new PermissionEndpointFilter(permission));
    }
}