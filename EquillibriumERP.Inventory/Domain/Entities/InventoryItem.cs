namespace EquillibriumERP.Inventory.Domain.Entities;

public class InventoryItem
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public decimal QuantityOnHand { get; set; }

    public decimal QuantityReserved { get; set; }

    public decimal ReorderLevel { get; set; }
}