using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Core.Abstractions.Authorization;

namespace EquillibriumERP.Products.Auth;

public sealed class ProductsPermissionProvider
    : IModulePermissionProvider
{
    public IEnumerable<PermissionDefinition> GetPermissions()
        => ProductsPermissions.All;
}