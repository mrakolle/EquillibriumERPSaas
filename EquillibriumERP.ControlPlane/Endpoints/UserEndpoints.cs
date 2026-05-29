using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;

namespace EquillibriumERP.ControlPlane.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/users/register",
        async (
            RegisterUserRequest request,
            MasterDbContext db) =>
        {
            var existingUser = await db.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existingUser is not null)
            {
                return Results.BadRequest("User already exists.");
            }

            var hasher = new PasswordHasher<User>();

            var user = new User
            {
                Id = Guid.NewGuid(),
                TenantId = request.TenantId,
                Email = request.Email
            };

            user.PasswordHash = hasher.HashPassword(
                user,
                request.Password
            );

            db.Users.Add(user);

            await db.SaveChangesAsync();

            return Results.Ok(new
            {
                user.Id,
                user.Email,
                user.TenantId
            });
        })
        .AllowAnonymous();
    }

    public record RegisterUserRequest(
        Guid TenantId,
        string Email,
        string Password
    );
}