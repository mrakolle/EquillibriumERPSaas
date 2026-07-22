using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Domain.Entities;
using EquillibriumERP.Sales.Infrastructure.Persistence;
using EquillibriumERP.Sales.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace EquillibriumERP.Sales.Services;

public class CustomerContactService : ICustomerContactService
{
    private readonly SalesDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public CustomerContactService(
        SalesDbContext db,
        ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task<IReadOnlyList<CustomerContactDto>> GetAllAsync(
        Guid customerId,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        return await _db.CustomerContacts
            .AsNoTracking()
            .Where(x => x.CustomerId == customerId)
            .OrderByDescending(x => x.IsPrimary)
            .ThenBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .Select(x => Map(x))
            .ToListAsync(ct);
    }

    public async Task<CustomerContactDto?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var entity = await _db.CustomerContacts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return entity is null
            ? null
            : Map(entity);
    }

    public async Task<CustomerContactDto> CreateAsync(
        CreateCustomerContactRequest request,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var entity = new CustomerContact(
            request.CustomerId,
            request.FirstName,
            request.LastName,
            request.Position,
            request.Email,
            request.Phone,
            request.IsPrimary);

        _db.CustomerContacts.Add(entity);

        await _db.SaveChangesAsync(ct);

        return Map(entity);
    }

    public async Task<CustomerContactDto> UpdateAsync(
        Guid id,
        UpdateCustomerContactRequest request,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var entity = await _db.CustomerContacts
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (entity is null)
        {
            throw new Exception("Customer contact not found.");
        }

        entity.Update(
            request.FirstName,
            request.LastName,
            request.Position,
            request.Email,
            request.Phone,
            request.IsPrimary);

        await _db.SaveChangesAsync(ct);

        return Map(entity);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var entity = await _db.CustomerContacts
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (entity is null)
        {
            return;
        }

        _db.CustomerContacts.Remove(entity);

        await _db.SaveChangesAsync(ct);
    }

    private static CustomerContactDto Map(CustomerContact entity)
    {
        return new CustomerContactDto
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Position = entity.Position,
            Email = entity.Email,
            Phone = entity.Phone,
            IsPrimary = entity.IsPrimary
        };
    }
}