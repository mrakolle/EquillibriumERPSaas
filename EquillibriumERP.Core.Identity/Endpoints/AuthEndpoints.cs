using EquillibriumERP.Core.Abstractions.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EquillibriumERP.Core.Identity.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(RouteGroupBuilder group)
    {
        //var group = app.MapGroup("/Auth");

        group.MapPost("auth/users", async (
            CreateUserRequest request,
            IUserService userService,
            CancellationToken ct) =>
        {
            var result = await userService.CreateAsync(request, ct);

            return Results.Ok(result);
        });
        
       group.MapPost("auth/login", async (
            LoginRequest request,
            ITenantAuthenticationService authService,
            CancellationToken ct) =>
        {
            var result = await authService.LoginAsync(request, ct);

            return Results.Ok(result);
        });
    }
}