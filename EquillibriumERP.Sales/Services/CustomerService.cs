using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Domain.Entities;
using EquillibriumERP.Sales.Infrastructure.Persistence;
using EquillibriumERP.Sales.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Sales.Services;

public class CustomerService : ICustomerService
{
    private readonly SalesDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public CustomerService(
        SalesDbContext db,
        ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        return await _db.Customers
        .AsNoTracking()
        .OrderBy(x => x.Name)
        .Select(x => new CustomerDto(
            x.Id,
            x.CustomerCode,
            x.Name,
            x.CustomerType,
            x.Email,
            x.Phone,
            x.IsActive
        ))
        .ToListAsync();
    }

    public async Task<CustomerDetailDto?> GetByIdAsync(Guid id)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        var customer = await _db.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return customer is null
            ? null
            : MapDetail(customer);
    }

    public async Task<CustomerDetailDto> CreateAsync(
        CreateCustomerRequest dto,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var customer = new Customer(
            dto.CustomerCode,
            dto.Name,
            dto.CustomerType,
            dto.CustomerCategoryId,
            dto.RegistrationNumber,
            dto.VatNumber,
            dto.TaxNumber,
            dto.Email,
            dto.Phone,
            dto.Mobile,
            dto.Website,
            dto.CreditLimit,
            dto.PaymentTerms
        );

        if (!dto.IsActive)
            customer.Deactivate();

        _db.Customers.Add(customer);

        await _db.SaveChangesAsync(ct);

        return MapDetail(customer);
    }

    public async Task<CustomerDetailDto?> UpdateAsync(
        Guid id,
        UpdateCustomerRequest dto,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var customer = await _db.Customers
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (customer is null)
            return null;

        customer.Update(
            dto.CustomerCode,
            dto.Name,
            dto.CustomerType,
            dto.CustomerCategoryId,
            dto.RegistrationNumber,
            dto.VatNumber,
            dto.TaxNumber,
            dto.Email,
            dto.Phone,
            dto.Mobile,
            dto.Website,
            dto.CreditLimit,
            dto.PaymentTerms
        );

        if (dto.IsActive)
            customer.Activate();
        else
            customer.Deactivate();

        await _db.SaveChangesAsync(ct);

        return MapDetail(customer);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        var customer = await _db.Customers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (customer is null)
            return false;

        _db.Customers.Remove(customer);

        await _db.SaveChangesAsync();

        return true;
    }

    private static CustomerDto Map(Customer customer)
    {
        return new CustomerDto(
            customer.Id,
            customer.CustomerCode,
            customer.Name,
            customer.CustomerType,
            customer.Email,
            customer.Phone,
            customer.IsActive
        );
    }

    private static CustomerDetailDto MapDetail(Customer customer)
    {
        return new CustomerDetailDto(
            customer.Id,
            customer.CustomerCode,
            customer.Name,
            customer.CustomerType,
            customer.CustomerCategoryId,
            customer.RegistrationNumber,
            customer.VatNumber,
            customer.TaxNumber,
            customer.Email,
            customer.Phone,
            customer.Mobile,
            customer.Website,
            customer.CreditLimit,
            customer.PaymentTerms,
            customer.IsActive
        );
    }
}