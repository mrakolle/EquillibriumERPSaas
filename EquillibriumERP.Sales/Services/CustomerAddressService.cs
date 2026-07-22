
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Domain.Entities;
using EquillibriumERP.Sales.Infrastructure.Persistence;
using EquillibriumERP.Sales.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Sales.Services;

public class CustomerAddressService : ICustomerAddressService
{
    private readonly SalesDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public CustomerAddressService(
        SalesDbContext db,
        ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task<IReadOnlyList<CustomerAddressDto>> GetAllAsync(
        Guid customerId,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        return await _db.CustomerAddresses
            .AsNoTracking()
            .Where(x => x.CustomerId == customerId)
            .OrderByDescending(x => x.IsPrimary)
            .ThenBy(x => x.AddressType)
            .Select(x => Map(x))
            .ToListAsync(ct);
    }

    public async Task<CustomerAddressDto?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var entity = await _db.CustomerAddresses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return entity is null
            ? null
            : Map(entity);
    }

    public async Task<CustomerAddressDto> CreateAsync(
        CreateCustomerAddressRequest request,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var entity = new CustomerAddress(
            request.CustomerId,
            request.AddressType,
            request.AddressLine1,
            request.AddressLine2,
            request.City,
            request.Province,
            request.PostalCode,
            request.Country,
            request.IsPrimary);

        _db.CustomerAddresses.Add(entity);

        await _db.SaveChangesAsync(ct);

        return Map(entity);
    }

    public async Task<CustomerAddressDto> UpdateAsync(
        Guid id,
        UpdateCustomerAddressRequest request,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var entity = await _db.CustomerAddresses
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (entity is null)
        {
            throw new Exception("Customer address not found.");
        }

        entity.Update(
            request.AddressType,
            request.AddressLine1,
            request.AddressLine2,
            request.City,
            request.Province,
            request.PostalCode,
            request.Country,
            request.IsPrimary);

        await _db.SaveChangesAsync(ct);

        return Map(entity);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var entity = await _db.CustomerAddresses
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (entity is null)
        {
            return;
        }

        _db.CustomerAddresses.Remove(entity);

        await _db.SaveChangesAsync(ct);
    }

    private static CustomerAddressDto Map(CustomerAddress entity)
    {
        return new CustomerAddressDto
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId,
            AddressType = entity.AddressType,
            AddressLine1 = entity.AddressLine1,
            AddressLine2 = entity.AddressLine2,
            City = entity.City,
            Province = entity.Province,
            PostalCode = entity.PostalCode,
            Country = entity.Country,
            IsPrimary = entity.IsPrimary
        };
    }
}