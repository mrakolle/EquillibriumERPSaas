using EquillibriumERP.Core.Identity.Domain.Entities;
using EquillibriumERP.Core.Identity.Infrastructure;

namespace EquillibriumERP.Core.Identity.Services;

public class PermissionService
{
    private readonly IdentityDbContext _db;

    public PermissionService(IdentityDbContext db)
    {
        _db = db;
    }

    public async Task<Permission> CreateAsync(string name, string? description = null)
    {
        var exists = _db.Permissions.Any(x => x.Name == name);
        if (exists)
            throw new Exception("Permission already exists");

        var permission = new Permission
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };

        _db.Permissions.Add(permission);
        await _db.SaveChangesAsync();

        return permission;
    }

    public async Task AttachToRoleAsync(Guid roleId, Guid permissionId)
    {
        var exists = _db.RolePermissions.Any(x =>
            x.RoleId == roleId && x.PermissionId == permissionId);

        if (exists) return;

        _db.RolePermissions.Add(new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        });

        await _db.SaveChangesAsync();
    }
}