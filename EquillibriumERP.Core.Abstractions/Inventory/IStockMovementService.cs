namespace EquillibriumERP.Core.Abstractions.Inventory;

public interface IStockMovementService
{
    Task ReceiveStockAsync(
        StockReceiveRequest request,
        CancellationToken cancellationToken = default);
}