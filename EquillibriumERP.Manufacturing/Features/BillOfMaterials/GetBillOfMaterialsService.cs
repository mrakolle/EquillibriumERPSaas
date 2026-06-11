using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Manufacturing.Application.Features.BillOfMaterials;

public class GetBillOfMaterialsService
{
    private readonly TenantDbContext _db;

    public GetBillOfMaterialsService(TenantDbContext db)
    {
        _db = db;
    }

    public async Task<List<BillOfMaterial>> Execute(CancellationToken ct)
    {
        return await _db.Set<BillOfMaterial>()
            .Include(x => x.Items)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}