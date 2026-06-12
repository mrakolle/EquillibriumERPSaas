using EquillibriumERP.Core.Abstractions.Inventory;
using EquillibriumERP.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Inventory.Services;

public sealed class StockMovementService
    : IStockMovementService
{
    private readonly InventoryDbContext _db;

    public StockMovementService(
        InventoryDbContext db)
    {
        _db = db;
    }

    public async Task ReceiveStockAsync(
        StockReceiveRequest request,
        CancellationToken ct = default)
    {
        var item = await _db.InventoryItems
            .FirstOrDefaultAsync(
                x => x.ProductId == request.ProductId,
                ct);

        if (item is null)
        {
            item = new InventoryItem
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                QuantityOnHand = 0,
                QuantityReserved = 0,
                ReorderLevel = 0
            };

            _db.InventoryItems.Add(item);
        }

        item.QuantityOnHand += request.Quantity;

        _db.InventoryTransactions.Add(
            new InventoryTransaction
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                LotNo = request.LotNo,
                TransactionType = "RECEIPT",
                ReferenceType = request.ReferenceType,
                ReferenceId = request.ReferenceId,
                TransactionDateUtc = DateTime.UtcNow
            });

        await _db.SaveChangesAsync(ct);
    }
}