using EquillibriumERP.Core.Abstractions.Authorization;

namespace EquillibriumERP.Core.Abstractions;

public interface IModulePermissionProvider
{
    IEnumerable<PermissionDefinition> GetPermissions();
}