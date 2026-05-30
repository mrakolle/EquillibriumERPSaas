using EquillibriumERP.Core.Infrastructure.Authorization;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.ControlPlane.Application.Roles;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.ControlPlane.Endpoints;

public static class RoleEndpoints
{
    public static void MapRoleEndpoints(WebApplication app)
    {
        app.MapPost("/api/controlplane/roles",
        async (
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

            var response = new RoleResponse(
                role.Id,
                role.Name,
                role.Description,
                role.Permissions
                    .Select(p => p.Code)
                    .ToList());

            return Results.Ok(response);
        });
    }
}
