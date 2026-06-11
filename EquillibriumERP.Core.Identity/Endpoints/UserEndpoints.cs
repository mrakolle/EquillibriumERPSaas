using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EquillibriumERP.Core.Identity.Application.Services;
using EquillibriumERP.Core.Identity.Domain.Entities;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Abstractions.Validations;
using EquillibriumERP.Identity.Application.Requests;
//using EquillibriumERP.ControlPlane.Contracts.Requests;
//using EquillibriumERP.ControlPlane.Contracts.Responses;


namespace EquillibriumERP.Core.Identity.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/identity/users")
            .WithTags("Users");

        MapCreateUser(group);
        MapGetUsers(group);
        MapGetUserById(group);
        MapUpdateUser(group);
        MapDeleteUser(group);
    }

    // CREATE
    private static void MapCreateUser(RouteGroupBuilder group)
    {
        group.MapPost("/", async (
            [FromBody] CreateUserRequest request,
            [FromServices] UserService service,
                CancellationToken ct) =>
        {
            ValidationHelper.EnsureNoDuplicatesOrThrow(request.RoleIds, "Duplicate roles not allowed");

           /* var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = "TEMP",
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            };

            db.Set<User>().Add(user);

            await db.SaveChangesAsync();

            if (request.RoleIds.Any())
            {
                var roles = request.RoleIds.Select(roleId => new UserRole
                {
                    UserId = user.Id,
                    RoleId = roleId
                });

                await db.Set<UserRole>().AddRangeAsync(roles);
                await db.SaveChangesAsync();
            }

            return Results.Ok(user.Id);*/
        });
    }

    // GET ALL
    private static void MapGetUsers(RouteGroupBuilder group)
    {
        group.MapGet("/", async (TenantDbContext db) =>
        {
           /* var users = await db.Set<User>()
                .Include(u => u.UserRoles)
                .Select(u => new UserResponse(
                    u.Id,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.IsActive,
                    u.UserRoles.Select(r => r.RoleId).ToList()
                ))
                .ToListAsync();

            return Results.Ok(users);*/
        });
    }

    // GET BY ID
    private static void MapGetUserById(RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (Guid id, TenantDbContext db) =>
        {
            /*var user = await db.Set<User>()
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return Results.NotFound();

            return Results.Ok(new UserResponse(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.IsActive,
                user.UserRoles.Select(r => r.RoleId).ToList()
            ));*/
        });
    }

    // UPDATE
    private static void MapUpdateUser(RouteGroupBuilder group)
    {
        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateUserRequest request,
            TenantDbContext db) =>
        {
            ValidationHelper.EnsureNoDuplicatesOrThrow(request.RoleIds, "Duplicate roles not allowed");

            /*var user = await db.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
                return Results.NotFound();

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IsActive = request.IsActive;

            await db.Set<UserRole>()
                .Where(x => x.UserId == user.Id)
                .ExecuteDeleteAsync();

            var newRoles = request.RoleIds.Select(roleId => new UserRole
            {
                UserId = user.Id,
                RoleId = roleId
            });

            await db.Set<UserRole>().AddRangeAsync(newRoles);

            await db.SaveChangesAsync();

            return Results.Ok(user.Id);*/
        });
    }

    // DELETE
    private static void MapDeleteUser(RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", async (Guid id, TenantDbContext db) =>
        {
            /*var user = await db.Set<User>()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return Results.NotFound();

            await db.Set<UserRole>()
                .Where(x => x.UserId == user.Id)
                .ExecuteDeleteAsync();

            db.Set<User>().Remove(user);

            await db.SaveChangesAsync();

            return Results.NoContent();*/
        });
    }
}