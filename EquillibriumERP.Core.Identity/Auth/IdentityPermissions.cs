using EquillibriumERP.Core.Abstractions.Authorization;

namespace EquillibriumERP.Core.Identity;

public static class IdentityPermissions
{
    public static readonly PermissionDefinition ViewUsers =
        new(
            "identity.users.view",
            "View Users",
            "Allows viewing users");

    public static readonly PermissionDefinition CreateUsers =
        new(
            "identity.users.create",
            "Create Users",
            "Allows creating users");

    public static readonly PermissionDefinition UpdateUsers =
        new(
            "identity.users.update",
            "Update Users",
            "Allows updating users");

    public static readonly PermissionDefinition DeleteUsers =
        new(
            "identity.users.delete",
            "Delete Users",
            "Allows deleting users");

    public static readonly PermissionDefinition AssignRoles =
        new(
            "identity.roles.assign",
            "Assign Roles",
            "Allows assigning roles to users");

    public static readonly PermissionDefinition[] All =
    [
        ViewUsers,
        CreateUsers,
        UpdateUsers,
        DeleteUsers,
        AssignRoles
    ];
}