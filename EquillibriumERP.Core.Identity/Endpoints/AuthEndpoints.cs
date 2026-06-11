using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EquillibriumERP.Core.Identity.Application;
using EquillibriumERP.Core.Identity.Application.Requests;
using EquillibriumERP.Core.Identity.Application.Services;


namespace EquillibriumERP.Core.Identity.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
         Console.WriteLine("AUTH ENDPOINTS REGISTERED 333");
        var group = app.MapGroup("/auth").WithTags("Authentication");
        MapLoginEndpoint(group);
        MapUserEndpoints(group);
        MapRoleEndpoints(group);
        MapPermissionEndpoints(group);

        


        return app;
    }

    private static void MapLoginEndpoint(RouteGroupBuilder group)
    {
        group.MapPost("/login", async (
            [FromBody] LoginRequest request,
            [FromServices] AuthService authService,
            CancellationToken ct) =>
            {
                var result = await authService.LoginAsync(request);

                return Results.Ok(result);
            });
            
    }

    private static void MapPermissionEndpoints(RouteGroupBuilder group)
    {
        
    }

    private static void MapRoleEndpoints(RouteGroupBuilder group)
    {
        
    }

    private static void MapUserEndpoints(RouteGroupBuilder group)
    {
        
    }
}