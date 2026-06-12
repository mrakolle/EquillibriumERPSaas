namespace EquillibriumERP.Inventory.Contracts;

public sealed class ReceiveStockRequest
{
    public Guid ProductId { get; init; }

    public decimal Quantity { get; init; }

    public string? LotNo { get; init; }

    public string ReferenceType { get; init; } = string.Empty;

    public Guid ReferenceId { get; init; }
}