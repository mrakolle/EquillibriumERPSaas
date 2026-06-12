namespace EquillibriumERP.Inventory.Contracts;

public sealed class InventoryItemResponse
{
    public Guid ProductId { get; init; }

    public decimal QuantityOnHand { get; init; }

    public decimal QuantityReserved { get; init; }

    public decimal ReorderLevel { get; init; }
}