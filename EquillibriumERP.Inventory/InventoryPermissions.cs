using EquillibriumERP.Core.Abstractions.Authorization;

namespace EquillibriumERP.Inventory;

public static class InventoryPermissions
{
    public static readonly PermissionDefinition View =
        new(
            "inventory.view",
            "View Inventory",
            "Allows viewing inventory");

    public static readonly PermissionDefinition Receive =
        new(
            "inventory.receive",
            "Receive Stock",
            "Allows receiving stock");

    public static readonly PermissionDefinition Issue =
        new(
            "inventory.issue",
            "Issue Stock",
            "Allows issuing stock");

    public static readonly PermissionDefinition Adjust =
        new(
            "inventory.adjust",
            "Adjust Stock",
            "Allows stock adjustments");

    public static readonly PermissionDefinition[] All =
    [
        View,
        Receive,
        Issue,
        Adjust
    ];
}