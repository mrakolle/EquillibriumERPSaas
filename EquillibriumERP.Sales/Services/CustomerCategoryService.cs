using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Domain.Entities;
using EquillibriumERP.Sales.Infrastructure.Persistence;
using EquillibriumERP.Sales.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Sales.Services;

public class CustomerCategoryService : ICustomerCategoryService
{
    private readonly SalesDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public CustomerCategoryService(
        SalesDbContext db,
        ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task<List<CustomerCategoryDto>> GetAllAsync()
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        return await _db.CustomerCategories
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .Select(x => new CustomerCategoryDto(
                x.Id,
                x.Code,
                x.Name,
                x.IsActive))
            .ToListAsync();
    }

    public async Task<CustomerCategoryDetailDto?> GetByIdAsync(Guid id)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        var category = await _db.CustomerCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category is null)
            return null;

        return Map(category);
    }

    public async Task<CustomerCategoryDetailDto> CreateAsync(
        CreateCustomerCategoryRequest request,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var category = new CustomerCategory(
            request.Code,
            request.Name,
            request.Description,
            request.SortOrder);

        _db.CustomerCategories.Add(category);

        await _db.SaveChangesAsync(ct);

        return Map(category);
    }

    public async Task<CustomerCategoryDetailDto?> UpdateAsync(
        Guid id,
        UpdateCustomerCategoryRequest request,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var category = await _db.CustomerCategories
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (category is null)
            return null;

        category.Update(
            request.Code,
            request.Name,
            request.Description,
            request.SortOrder);

        if (request.IsActive)
            category.Activate();
        else
            category.Deactivate();

        await _db.SaveChangesAsync(ct);

        return Map(category);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        var category = await _db.CustomerCategories
            .Include(x => x.Customers)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category is null)
            return false;

        if (category.Customers.Any())
            throw new InvalidOperationException(
                "This category is assigned to one or more customers.");

        _db.CustomerCategories.Remove(category);

        await _db.SaveChangesAsync();

        return true;
    }

    private static CustomerCategoryDetailDto Map(
        CustomerCategory category)
    {
        return new CustomerCategoryDetailDto(
            category.Id,
            category.Code,
            category.Name,
            category.Description,
            category.SortOrder,
            category.IsActive);
    }
}