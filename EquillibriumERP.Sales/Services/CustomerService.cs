using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Sales.Interfaces;
using EquillibriumERP.Sales.Infrastructure.Persistence;
using EquillibriumERP.Sales.Contracts.Customers;
using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace EquillibriumERP.Sales.Services;

public sealed class CustomerService
    : ICustomerService
{
    private readonly SalesDbContext _db;
    private readonly ITenantResolver _tenantResolver;
    private readonly ITenantSession _tenantSession;
    private readonly ITenantContextualizer _tenantContextualizer;

    public CustomerService(
        SalesDbContext db,
        ITenantResolver tenantResolver, ITenantContextualizer tenantContextualizer, ITenantSession tenantSession)
    {
        _db = db;
        _tenantResolver = tenantResolver;
        _tenantContextualizer = tenantContextualizer;
        _tenantSession = tenantSession;
        
    }

    public async Task<CustomerDto> CreateAsync(
    CreateCustomerRequest request,
    CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db,ct);

        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email
        };

        _db.Customers.Add(customer);

        await _db.SaveChangesAsync(ct);

        return new CustomerDto(
            customer.Id,
            customer.Name,
            customer.Email);
    }
    public async Task<CustomerDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db,ct);

        var customer = await _db.Customers.FindAsync([id], ct);

        return customer is null
            ? null
            : new CustomerDto(
                customer.Id,
                customer.Name,
                customer.Email);
    }
}