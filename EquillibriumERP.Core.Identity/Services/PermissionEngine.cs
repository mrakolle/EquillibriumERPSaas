using EquillibriumERP.Core.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Core.Identity.Application.Services;

public class PermissionEngine
{
    private readonly IdentityDbContext _db;

    public PermissionEngine(IdentityDbContext db)
    {
        _db = db;
    }

    public async Task<UserClaimsResult> GetUserClaimsAsync(Guid userId, Guid tenantId)
    {
        var roles = await _db.UserRoles
            .Where(x => x.UserId == userId)
            .Join(_db.Roles,
                ur => ur.RoleId,
                r => r.Id,
                (ur, r) => r.Name!)
            .ToListAsync();

        var permissions = await (
            from rp in _db.RolePermissions
            join r in _db.Roles on rp.RoleId equals r.Id
            join p in _db.Permissions on rp.PermissionId equals p.Id
            where roles.Contains(r.Name!)
            select p.Name
        )
        .Distinct()
        .ToListAsync();

        return new UserClaimsResult
        {
            UserId = userId,
            TenantId = tenantId,
            Roles = roles,
            Permissions = permissions
        };
    }

    public async Task<bool> HasPermissionAsync(Guid userId, Guid tenantId, string permission)
    {
        var claims = await GetUserClaimsAsync(userId, tenantId);

        return claims.Permissions.Contains(permission);
    }

    public async Task<bool> HasRoleAsync(Guid userId, Guid tenantId, string role)
    {
        var claims = await GetUserClaimsAsync(userId, tenantId);

        return claims.Roles.Contains(role);
    }
}