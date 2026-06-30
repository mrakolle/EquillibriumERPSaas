using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Abstractions.Authorization;


namespace EquillibriumERP.Core.Identity;

public sealed class IdentityPermissionProvider : IModulePermissionProvider
{
    public IEnumerable<PermissionDefinition> GetPermissions()
        => IdentityPermissions.All;
}