using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Purchasing.Contracts;
using EquillibriumERP.Purchasing.Interfaces;
using EquillibriumERP.Purchasing.Domain.Entities;
using EquillibriumERP.Purchasing.Infrastructure.Persistence;

namespace EquillibriumERP.Purchasing.Services;

public class SupplierService : ISupplierService
{
    private readonly PurchasingDbContext _db;
    private readonly ITenantResolver _tenantResolver;
    private readonly ITenantSession _tenantSession;
    private readonly ITenantContextualizer _tenantContextualizer;

    public SupplierService(
        PurchasingDbContext db,
        ITenantResolver tenantResolver,
        ITenantContextualizer tenantContextualizer,
        ITenantSession tenantSession)
    {
        _db = db;
        _tenantResolver = tenantResolver;
        _tenantContextualizer = tenantContextualizer;
        _tenantSession = tenantSession;
    }

    public async Task<SupplierDto> CreateAsync(
        CreateSupplierRequest request,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var nextNumber = await _db.Set<Supplier>().CountAsync(ct) + 1;
        var supplierCode = $"SUP-{nextNumber:D6}";

        var supplier = new Supplier(
            supplierCode,
            request.Name,
            request.SupplierCategoryId,
            request.RegistrationNumber,
            request.VatNumber,
            request.TaxNumber,
            request.Email,
            request.Phone,
            request.Mobile,
            request.Website,
            request.PaymentTerms);

        _db.Set<Supplier>().Add(supplier);

        await _db.SaveChangesAsync(ct);

        return Map(supplier);
    }

    public async Task<List<SupplierDto>> GetAllAsync()
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        var suppliers = await _db.Set<Supplier>()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();

        return suppliers.Select(Map).ToList();
    }

    public async Task<SupplierDto?> GetByIdAsync(Guid id)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        var supplier = await _db.Set<Supplier>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return supplier is null ? null : Map(supplier);
    }

   public async Task<SupplierDto?> UpdateAsync(
    Guid id,
    UpdateSupplierRequest request,
    CancellationToken ct = default)
{
    await _tenantContextualizer.SetTenantContextAsync(_db, ct);

    var supplier = await _db.Set<Supplier>()
        .FirstOrDefaultAsync(x => x.Id == id, ct);

    if (supplier is null)
        return null;

    supplier.Update(
        supplier.SupplierCode,
        request.Name,
        request.SupplierCategoryId,
        request.RegistrationNumber,
        request.VatNumber,
        request.TaxNumber,
        request.Email,
        request.Phone,
        request.Mobile,
        request.Website,
        request.PaymentTerms);

    await _db.SaveChangesAsync(ct);

    return Map(supplier);
}

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

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
            supplier.SupplierCode,
            supplier.Name,
            supplier.SupplierCategoryId,
            supplier.RegistrationNumber,
            supplier.VatNumber,
            supplier.TaxNumber,
            supplier.Email,
            supplier.Phone,
            supplier.Mobile,
            supplier.Website,
            supplier.PaymentTerms,
            supplier.IsActive);
    }
}