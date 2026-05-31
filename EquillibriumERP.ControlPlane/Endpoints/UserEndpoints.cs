using EquillibriumERP.Core.Infrastructure.Authorization;
using EquillibriumERP.ControlPlane.Contracts.Requests;
using EquillibriumERP.ControlPlane.Contracts.Responses;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;
using EquillibriumERP.Core.Infrastructure.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using EquillibriumERP.Core.Abstractions.Validations;
using EquillibriumERP.ControlPlane.Application.Contracts.Requests;
using EquillibriumERP.ControlPlane.Application.Contracts.Responses;


namespace EquillibriumERP.ControlPlane.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/api/controlplane/users")
            .WithTags("Users");

        MapCreateUser(group);
        MapGetUsers(group);
        MapGetUserById(group);
        MapUpdateUser(group);
        MapDeleteUser(group);
    }

    private static void MapCreateUser(RouteGroupBuilder group)
    {
        // CREATE
       group.MapPost("/", async (
            CreateUserRequest request,
            MasterDbContext db) =>
            
        {
            ValidationHelper.EnsureNoDuplicatesOrThrow(request.RoleIds, "Duplicate roles not allowed");
            var user = new User
            {
                Id = Guid.NewGuid(),
                TenantId = Guid.NewGuid(), // replace with real tenant resolver later
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = "TEMP", // replace with auth later
                IsActive = true
            };

            db.Users.Add(user);

            await db.SaveChangesAsync();

            // IMPORTANT: roles handled AFTER user exists
            if (request.RoleIds.Any())
            {
                var userRoles = request.RoleIds.Select(roleId => new UserRole
                {
                    UserId = user.Id,
                    RoleId = roleId
                });

                await db.UserRoles.AddRangeAsync(userRoles);
                await db.SaveChangesAsync();
            }

            return Results.Ok(user.Id);
        });
    }

    private static void MapGetUsers(RouteGroupBuilder group)
    {
         // GET ALL
        group.MapGet("/", async (MasterDbContext db) =>
        {
            var users = await db.Users
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

            return Results.Ok(users);
        });
    }

    private static void MapGetUserById(RouteGroupBuilder group)
    {
       // GET BY ID
        group.MapGet("/{id:guid}", async (Guid id, MasterDbContext db) =>
        {
            var user = await db.Users
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
            ));
        });
    }

    private static void MapUpdateUser(RouteGroupBuilder group)
    {
        // UPDATE
        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateUserRequest request,
            MasterDbContext db) =>
        {
            ValidationHelper.EnsureNoDuplicatesOrThrow(request.RoleIds, "Duplicate roles not allowed");
            var user = await db.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
                return Results.NotFound();

            // 1. Update scalar fields
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IsActive = request.IsActive;

            // 2. HARD RESET ROLE LINKS (NO TRACKING ISSUES EVER)
            await db.UserRoles
                .Where(x => x.UserId == user.Id)
                .ExecuteDeleteAsync();

            // 3. Reinsert roles (clean state)
            var newRoles = request.RoleIds.Select(roleId => new UserRole
            {
                UserId = user.Id,
                RoleId = roleId
            });

            await db.UserRoles.AddRangeAsync(newRoles);

            // 4. Save once
            await db.SaveChangesAsync();

            return Results.Ok(user.Id);
        });
    }

    private static void MapDeleteUser(RouteGroupBuilder group)
    {
         // DELETE
        group.MapDelete("/{id:guid}", async (Guid id, MasterDbContext db) =>
        {
            var user = await db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return Results.NotFound();

            await db.UserRoles
                .Where(x => x.UserId == user.Id)
                .ExecuteDeleteAsync();

            db.Users.Remove(user);

            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}