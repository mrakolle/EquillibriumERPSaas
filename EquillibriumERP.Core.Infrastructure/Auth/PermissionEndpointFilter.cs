using Microsoft.AspNetCore.Http;

namespace EquillibriumERP.Core.Infrastructure.Authorization;

public class PermissionEndpointFilter : IEndpointFilter
{
    private readonly string _permission;

    public PermissionEndpointFilter(string permission)
    {
        _permission = permission;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var user = context.HttpContext.User;

        var hasPermission = user.Claims.Any(c =>
            c.Type == "permission" && c.Value == _permission);

        if (!hasPermission)
        {
            return Results.Forbid();
        }

        return await next(context);
    }
}