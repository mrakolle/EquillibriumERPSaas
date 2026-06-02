using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Identity.Infrastructure.Entities;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.ControlPlane.Contracts.Requests;
using EquillibriumERP.ControlPlane.Contracts.Responses;



namespace EquillibriumERP.ControlPlane.Endpoints;

public static class RoleEndpoints
{
    public static void MapRoleEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/api/controlplane/roles")
            .WithTags("Roles");

        MapCreateRole(group);
        MapGetRoles(group);
        MapGetRoleById(group);
        MapUpdateRole(group);
        MapDeleteRole(group);
    }

    private static void MapCreateRole(RouteGroupBuilder group)
    {
        group.MapPost("/", async (
            CreateRoleRequest request,
            TenantDbContext db) =>
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            role.RolePermissions = request.Permissions.Select(p => new RolePermission
            {
                RoleId = role.Id,
                Permission = new Permission
                {
                    Id = Guid.NewGuid(),
                    Code = p,
                    Name = p
                }
            }).ToList();

            db.Set<Role>().Add(role);
            await db.SaveChangesAsync();

            return Results.Ok(role.Id);
        });
    }

    private static void MapGetRoles(RouteGroupBuilder group)
    {
        group.MapGet("/", async (TenantDbContext db) =>
        {
            var roles = await db.Set<Role>()
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .Select(r => new RoleResponse(
                    r.Id,
                    r.Name,
                    r.RolePermissions
                        .Select(p => p.Permission.Code)
                        .ToList()
                ))
                .ToListAsync();

            return Results.Ok(roles);
        });
    }

    private static void MapGetRoleById(RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (Guid id, TenantDbContext db) =>
        {
            var role = await db.Set<Role>()
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
                return Results.NotFound();

            return Results.Ok(new RoleResponse(
                role.Id,
                role.Name,
                role.RolePermissions.Select(x => x.Permission.Code).ToList()
            ));
        });
    }

    private static void MapUpdateRole(RouteGroupBuilder group)
    {
        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateRoleRequest request,
            TenantDbContext db) =>
        {
            var role = await db.Set<Role>()
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
                return Results.NotFound();

            role.Name = request.Name;

            await db.Set<RolePermission>()
                .Where(x => x.RoleId == role.Id)
                .ExecuteDeleteAsync();

            role.RolePermissions = request.Permissions.Select(p => new RolePermission
            {
                RoleId = role.Id,
                Permission = new Permission
                {
                    Id = Guid.NewGuid(),
                    Code = p,
                    Name = p
                }
            }).ToList();

            await db.SaveChangesAsync();

            return Results.Ok(role.Id);
        });
    }

    private static void MapDeleteRole(RouteGroupBuilder group)
    {
        group.MapDelete("/{id:guid}", async (Guid id, TenantDbContext db) =>
        {
            var role = await db.Set<Role>()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
                return Results.NotFound();

            await db.Set<RolePermission>()
                .Where(x => x.RoleId == id)
                .ExecuteDeleteAsync();

            db.Set<Role>().Remove(role);

            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}