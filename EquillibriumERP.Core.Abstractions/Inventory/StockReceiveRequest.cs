namespace EquillibriumERP.Core.Abstractions.Inventory;

public sealed class StockReceiveRequest
{
    public Guid ProductId { get; init; }

    public decimal Quantity { get; init; }

    public string? LotNo { get; init; }

    public string ReferenceType { get; init; } = string.Empty;

    public Guid ReferenceId { get; init; }
}