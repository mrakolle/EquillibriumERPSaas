using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Infrastructure.Persistence;

namespace EquillibriumERP.Core.Infrastructure.MultiTenancy;
public sealed class TenantLookup : ITenantLookup
{
    private readonly MasterDbContext _db;

    public TenantLookup(MasterDbContext db)
    {
        _db = db;
    }

    public async Task<TenantInfo?> GetByCodeAsync(string code, CancellationToken ct)
    {
        return await _db.Tenants
            .Where(x => x.Code == code)
            .Select(x => new TenantInfo
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Schema = x.Schema
            })
            .FirstOrDefaultAsync(ct);
    }
}