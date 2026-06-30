using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Purchasing.Contracts;
using EquillibriumERP.Purchasing.Interfaces;
using EquillibriumERP.Purchasing.Domain.Entities;
using EquillibriumERP.Purchasing.Domain.Enums;
using EquillibriumERP.Purchasing.Infrastructure.Persistence;
using EquillibriumERP.Core.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace EquillibriumERP.Purchasing.Services;

public class SupplierService : ISupplierService
{
    private readonly PurchasingDbContext _db;
    private readonly ITenantResolver _tenantResolver;
    private readonly ITenantSession _tenantSession;
    private readonly ITenantContextualizer _tenantContextualizer;

    public SupplierService(
        PurchasingDbContext db,
        ITenantResolver tenantResolver, ITenantContextualizer tenantContextualizer, ITenantSession tenantSession)
    {
        _db = db;
        _tenantResolver = tenantResolver;
        _tenantContextualizer = tenantContextualizer;
        _tenantSession = tenantSession;
        
    }

    public async Task<SupplierDto> CreateAsync(
    CreateSupplierRequest dto,
    CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var supplier = new Supplier
        {
            SupplierCode = dto.SupplierCode,
            Name = dto.Name,
            ContactPerson = dto.ContactPerson,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            IsActive = true
        };

        _db.Set<Supplier>().Add(supplier);

        await _db.SaveChangesAsync(ct);

        return Map(supplier);
    }

    public async Task<List<SupplierDto>> GetAllAsync()
    {
        CancellationToken ct = default;
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);
        return await _db.Set<Supplier>()
            .AsNoTracking()
            .Select(p => Map(p))
            .ToListAsync();
    }

    public async Task<SupplierDto?> GetByIdAsync(Guid id)
    {
        CancellationToken ct = default;
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var supplier = await _db.Set<Supplier>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return supplier is null
            ? null
            : Map(supplier);
    }

    public async Task<SupplierDto?> UpdateAsync(
    Guid id,
    UpdateSupplierRequest dto)
    {
        CancellationToken ct = default;

        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var supplier = await _db.Set<Supplier>()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (supplier is null)
            return null;

        supplier.SupplierCode = dto.SupplierCode;
        supplier.Name = dto.Name;
        supplier.ContactPerson = dto.ContactPerson;
        supplier.Email = dto.Email;
        supplier.PhoneNumber = dto.PhoneNumber;
        supplier.IsActive = dto.IsActive;

        await _db.SaveChangesAsync(ct);

        return Map(supplier);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        CancellationToken ct = default;
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var supplier = await _db.Set<Supplier>()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (supplier is null)
            return false;
        

        _db.Set<Supplier>().Remove(supplier);

        await _db.SaveChangesAsync();

        return true;
    }
    private static SupplierDto Map(Supplier supplier)
    {
        return new SupplierDto(
            supplier.Id,
            supplier.Name,
            supplier.SupplierCode,
            supplier.ContactPerson,
            supplier.Email,
            supplier.PhoneNumber,
            supplier.IsActive
        );
    }
}
