using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Core.Abstractions.Authorization;

namespace EquillibriumERP.Purchasing.Auth;

public sealed class PurchasingPermissionProvider
    : IModulePermissionProvider
{
    public IEnumerable<PermissionDefinition> GetPermissions()
        => PurchasingPermissions.All;
}