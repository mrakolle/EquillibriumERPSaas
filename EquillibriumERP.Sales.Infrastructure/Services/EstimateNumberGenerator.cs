using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Sales.Infrastructure.Services;

public class EstimateNumberGenerator
{
    private readonly ITenantDbContext _db;

    public EstimateNumberGenerator(ITenantDbContext db)
    {
        _db = db;
    }

    public async Task<string> GenerateAsync()
    {
        var year = DateTime.UtcNow.Year;

        var seq = await _db.Set<EstimateSequence>()
            .FirstOrDefaultAsync(x => x.Year == year);

        if (seq == null)
        {
            seq = new EstimateSequence(year);
            _db.Set<EstimateSequence>().Add(seq);
        }

        var next = seq.Next();

        var formatted = next.ToString("D6"); // 000001

        return $"EST-{year}-{formatted[..3]}-{formatted[3..]}";
    }
}