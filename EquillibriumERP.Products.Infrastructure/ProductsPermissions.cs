using EquillibriumERP.Core.Abstractions.Authorization;

namespace EquillibriumERP.Products.Infrastructure;

public static class ProductsPermissions
{
    public static readonly PermissionDefinition View =
        new(
            "products.view",
            "View Products",
            "Allows viewing products");

    public static readonly PermissionDefinition Create =
        new(
            "products.create",
            "Create Products",
            "Allows creating products");

    public static readonly PermissionDefinition Update =
        new(
            "products.update",
            "Update Products",
            "Allows updating products");

    public static readonly PermissionDefinition Delete =
        new(
            "products.delete",
            "Delete Products",
            "Allows deleting products");

    public static readonly PermissionDefinition[] All =
    [
        View,
        Create,
        Update,
        Delete
    ];
}