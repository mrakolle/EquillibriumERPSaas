using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Manufacturing.Domain.Entities;
using EquillibriumERP.Manufacturing.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Manufacturing.Application.Features.Batches;

public class BatchService
{
    private readonly TenantDbContext _db;
    private readonly BatchMovementService _movement;

    public BatchService(
        TenantDbContext db,
        BatchMovementService movement)
    {
        _db = db;
        _movement = movement;
    }

    public async Task ConsumeMaterial(
        Guid batchId,
        Guid rawMaterialId,
        decimal quantity,
        CancellationToken ct)
    {
        var batch = await _db.Set<Batch>()
            .Include(x => x.BillOfMaterial)
                .ThenInclude(x => x.Items)
            .Include(x => x.Consumptions)
            .FirstOrDefaultAsync(x => x.Id == batchId, ct);

        if (batch == null)
            throw new Exception("Batch not found.");

        _movement.ConsumeMaterial(
            batch,
            rawMaterialId,
            quantity);

        await _db.SaveChangesAsync(ct);
    }
}