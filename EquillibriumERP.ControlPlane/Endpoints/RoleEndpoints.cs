using EquillibriumERP.Core.Infrastructure.Authorization;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.ControlPlane.Application.Contracts.Requests;
using EquillibriumERP.ControlPlane.Application.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.ControlPlane.Endpoints;

public static class RoleEndpoints
{
    public static void MapRoleEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/api/controlplane/roles")
               .WithTags("Roles");

        MapCreateRole(group);
        MapGeteRole(group);
        MapGeteRoleById(group);
        MapUpdateRole(group);
        MapDeleteRole(group);

    }

    private static void MapDeleteRole(RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", async (
            Guid id,
            MasterDbContext db) =>
        {
            var role = await db.Roles
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
                return Results.NotFound();

            // 1. Delete children (fast SQL delete)
            await db.RolePermissions
                .Where(x => x.RoleId == role.Id)
                .ExecuteDeleteAsync();

            // 2. Delete parent
            db.Roles.Remove(role);

            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }

    private static void MapUpdateRole(RouteGroupBuilder group)
    {
       group.MapPut("/{id:guid}",
        async (
            Guid id,
            UpdateRoleRequest request,
            MasterDbContext db,
            IServiceProvider sp) =>
        {
            var validPermissions = PermissionRegistry
                .GetAll(sp)
                .Select(x => x.Code)
                .ToHashSet();

            var invalid = request.Permissions
                .Where(p => !validPermissions.Contains(p))
                .ToList();

            if (invalid.Any())
                return Results.BadRequest(new
                {
                    error = "Invalid permissions",
                    invalid
                });

            var role = await db.Roles
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
                return Results.NotFound();

            role.Name = request.Name;
            role.Description = request.Description;

            await db.RolePermissions
                .Where(x => x.RoleId == role.Id)
                .ExecuteDeleteAsync();

            var permissions = request.Permissions
                .Select(p => new RolePermission
                {
                    Id = Guid.NewGuid(),
                    RoleId = role.Id,
                    Code = p
                });

            await db.RolePermissions.AddRangeAsync(permissions);

            await db.SaveChangesAsync();

            return Results.Ok(new RoleResponse(
                role.Id,
                role.Name,
                role.Description,
                request.Permissions));
        });
    }

    private static void MapGeteRoleById(RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}",
        async (Guid id, MasterDbContext db) =>
        {
            var role = await db.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
                return Results.NotFound();

            return Results.Ok(
                new RoleResponse(
                    role.Id,
                    role.Name,
                    role.Description,
                    role.Permissions
                        .Select(p => p.Code)
                        .ToList()));
        });
    }

    private static void MapGeteRole(RouteGroupBuilder group)
    {
        group.MapGet("/", async (MasterDbContext db) =>
        {
            var roles = await db.Roles
                .Include(r => r.Permissions)
                .Select(r => new RoleResponse(
                    r.Id,
                    r.Name,
                    r.Description,
                    r.Permissions
                        .Select(p => p.Code)
                        .ToList()))
                .ToListAsync();

            return Results.Ok(roles);
        });
    }

    private static void MapCreateRole(RouteGroupBuilder group)
    {
        group.MapPost("/", async (
            CreateRoleRequest request,
            MasterDbContext db,
            IServiceProvider sp) =>
        {
            var validPermissions = PermissionRegistry
                .GetAll(sp)
                .Select(x => x.Code)
                .ToHashSet();

            var invalid = request.Permissions
                .Where(p => !validPermissions.Contains(p))
                .ToList();

            if (invalid.Any())
                return Results.BadRequest(new
                {
                    error = "Invalid permissions",
                    invalid
                });

            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            role.Permissions = request.Permissions
                .Select(p => new RolePermission
                {
                    Id = Guid.NewGuid(),
                    RoleId = role.Id,
                    Code = p
                })
                .ToList();

            db.Roles.Add(role);

            await db.SaveChangesAsync();

            return Results.Ok(new RoleResponse(
                role.Id,
                role.Name,
                role.Description,
                role.Permissions
                    .Select(p => p.Code)
                    .ToList()));
        });
    }
}
