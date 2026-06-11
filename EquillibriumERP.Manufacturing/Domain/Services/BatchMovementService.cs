using EquillibriumERP.Manufacturing.Domain.Entities;
using EquillibriumERP.Manufacturing.Domain.Enums;

namespace EquillibriumERP.Manufacturing.Domain.Services;

public class BatchMovementService
{
    public void ConsumeMaterial(
        Batch batch,
        Guid rawMaterialId,
        decimal quantity)
    {
        if (batch.Status != BatchStatus.InProgress)
            throw new InvalidOperationException("Batch must be in progress.");

        var bomItem = batch.BillOfMaterial.Items
            .FirstOrDefault(x => x.RawMaterialId == rawMaterialId);

        if (bomItem == null)
            throw new InvalidOperationException("Material not part of BOM.");

        var existing = batch.Consumptions
            .FirstOrDefault(x => x.RawMaterialId == rawMaterialId);

        if (existing == null)
        {
            batch.Consumptions.Add(new BatchConsumption
            {
                Id = Guid.NewGuid(),
                BatchId = batch.Id,
                RawMaterialId = rawMaterialId,
                Quantity = quantity,
                ConsumedAt = DateTime.UtcNow
            });

            return;
        }

        existing.Quantity += quantity;
    }
}