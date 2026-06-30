
using EquillibriumERP.Core.Abstractions.Authorization;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Identity.Infrastructure;

namespace EquillibriumERP.Core.Identity.Services;
public sealed class UserPermissionService
    : IPermissionService
{
    private readonly IdentityDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public UserPermissionService(
        IdentityDbContext db,
        ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }


public bool HasPermission(string userId, string permission)
{
    _tenantContextualizer
        .SetTenantContextAsync(_db)
        .GetAwaiter()
        .GetResult();

    var id = Guid.Parse(userId);

    return _db.UserRoles
        .Join(_db.RolePermissions,
            ur => ur.RoleId,
            rp => rp.RoleId,
            (ur, rp) => new { ur, rp })
        .Join(_db.Permissions,
            x => x.rp.PermissionId,
            p => p.Id,
            (x, p) => new { x.ur, p })
        .Any(x =>
            x.ur.UserId == id
            &&
           x.p.Name == permission);
}
}