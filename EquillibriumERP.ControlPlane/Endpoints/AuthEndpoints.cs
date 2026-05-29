using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using EquillibriumERP.Core.Infrastructure.Auth;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;

namespace EquillibriumERP.ControlPlane.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login",
        async (
            LoginRequest request,
            MasterDbContext db,
            JwtTokenService jwt) =>
        {
            var user = await db.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user is null)
                return Results.Unauthorized();

            var hasher = new PasswordHasher<User>();

            var passwordResult = hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password
            );

            if (passwordResult == PasswordVerificationResult.Failed)
                return Results.Unauthorized();

            var token = jwt.CreateToken(
                user.Id,
                user.TenantId,
                user.Email
            );

            return Results.Ok(new LoginResponse(
                token,
                user.Id,
                user.TenantId
            ));
        })
        .AllowAnonymous()
        .WithName("Login");
    }

    public record LoginRequest(string Email, string Password);

    public record LoginResponse(
        string Token,
        Guid UserId,
        Guid TenantId
    );
}