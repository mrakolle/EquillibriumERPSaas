using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Core.Abstractions.Authorization;

namespace EquillibriumERP.Inventory;

public sealed class InventoryPermissionProvider
    : IModulePermissionProvider
{
    public IEnumerable<PermissionDefinition> GetPermissions()
        => InventoryPermissions.All;
}