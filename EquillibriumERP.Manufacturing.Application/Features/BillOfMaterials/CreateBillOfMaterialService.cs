using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Manufacturing.Application.Features.BillOfMaterials;

public class BillOfMaterialsService
{
    private readonly TenantDbContext _db;

    public BillOfMaterialsService(TenantDbContext db)
    {
        _db = db;
    }

    public async Task<CreateBillOfMaterialResponse> Create(
        CreateBillOfMaterialRequest request,
        CancellationToken ct)
    {
        var bom = new BillOfMaterial
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            Code = request.Code,
            Description = request.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            Items = request.Items.Select(x => new BillOfMaterialItem
            {
                Id = Guid.NewGuid(),
                RawMaterialId = x.RawMaterialId,
                Quantity = x.Quantity,
                Unit = x.Unit,
                IsOptional = x.IsOptional
            }).ToList()
        };

        _db.Set<BillOfMaterial>().Add(bom);
        await _db.SaveChangesAsync(ct);

        return new CreateBillOfMaterialResponse
        {
            Id = bom.Id
        };
    }

    public async Task<List<GetBillOfMaterialsResponse>> GetAll(CancellationToken ct)
    {
        return await _db.Set<BillOfMaterial>()
            .AsNoTracking()
            .Select(x => new GetBillOfMaterialsResponse
            {
                Id = x.Id,
                Code = x.Code,
                ProductId = x.ProductId
            })
            .ToListAsync(ct);
    }
}