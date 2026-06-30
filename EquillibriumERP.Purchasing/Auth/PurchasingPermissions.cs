using EquillibriumERP.Core.Abstractions.Authorization;

namespace EquillibriumERP.Purchasing.Auth;

public static class PurchasingPermissions
{
    public static readonly PermissionDefinition ViewSuppliers =
        new(
            "purchasing.suppliers.view",
            "View Suppliers",
            "Allows viewing suppliers");

    public static readonly PermissionDefinition CreateSuppliers =
        new(
            "purchasing.suppliers.create",
            "Create Suppliers",
            "Allows creating suppliers");

    public static readonly PermissionDefinition ViewPurchaseOrders =
        new(
            "purchasing.purchaseorders.view",
            "View Purchase Orders",
            "Allows viewing purchase orders");

    public static readonly PermissionDefinition CreatePurchaseOrders =
        new(
            "purchasing.purchaseorders.create",
            "Create Purchase Orders",
            "Allows creating purchase orders");

    public static readonly PermissionDefinition ApprovePurchaseOrders =
        new(
            "purchasing.purchaseorders.approve",
            "Approve Purchase Orders",
            "Allows approving purchase orders");

    public static readonly PermissionDefinition ReceivePurchaseOrders =
        new(
            "purchasing.purchaseorders.receive",
            "Receive Purchase Orders",
            "Allows receiving purchase orders");

    public static readonly PermissionDefinition[] All =
    [
        ViewSuppliers,
        CreateSuppliers,
        ViewPurchaseOrders,
        CreatePurchaseOrders,
        ApprovePurchaseOrders,
        ReceivePurchaseOrders
    ];
}