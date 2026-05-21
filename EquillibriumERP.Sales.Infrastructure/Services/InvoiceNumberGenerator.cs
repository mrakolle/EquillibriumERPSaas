using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Abstractions.Persistence;
using EquillibriumERP.Abstractions.MultiTenancy;
using EquillibriumERP.Sales.Domain.Entities;

namespace EquillibriumERP.Sales.Infrastructure.Services;

public class InvoiceNumberGenerator
{
    private readonly ITenantDbContext _db;

    public InvoiceNumberGenerator(ITenantDbContext db)
    {
        _db = db;
    }

    public async Task<string> GenerateAsync()
    {
        var year = DateTime.UtcNow.Year;

        var seq = await _db.Set<InvoiceSequence>()
            .FirstOrDefaultAsync(x => x.Year == year);

        if (seq == null)
        {
            seq = new InvoiceSequence(year);
            _db.Set<InvoiceSequence>().Add(seq);
        }

        var next = seq.Next();

        var formatted = next.ToString("D6");

        return $"INV-{year}-{formatted[..3]}-{formatted[3..]}";
    }
}