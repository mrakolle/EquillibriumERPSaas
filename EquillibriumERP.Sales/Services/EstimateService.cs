using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Sales.Interfaces;
using EquillibriumERP.Sales.Contracts.Estimates;
using EquillibriumERP.Sales.Infrastructure.Persistence;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Sales.Domain.Entities;
using EquillibriumERP.Sales.Domain.Enums;
using EquillibriumERP.Core.Abstractions.Products;
using System.Collections.Concurrent;

namespace EquillibriumERP.Sales.Services;
public class EstimateService : IEstimateService
{
    private readonly SalesDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;
    private readonly IProductLookup _productLookup;

    public EstimateService(
    SalesDbContext db,
    ITenantContextualizer tenantContextualizer,
    IProductLookup productLookup)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
        _productLookup = productLookup;
    }

    public async Task<EstimateDto?> GetByIdAsync(
    Guid id,
    CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db,ct);

        return await _db.Estimates
            .Where(x => x.Id == id)
            .Select(x => new EstimateDto(
                x.Id,
                x.CustomerId,
                x.ReferenceNumber,
                x.EstimateDateUtc,
                x.Status,
                x.ExpiryDateUtc))
            .FirstOrDefaultAsync(ct);
    }
    public async Task<EstimateDto> CreateEstimateAsync(
    CreateEstimateRequest request,
    CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var items = new List<EstimateItem>();

        foreach (var i in request.Items)
        {
            var product = await _productLookup.GetByIdAsync(i.ProductId, ct);

            if (product is null)
                throw new Exception($"Product {i.ProductId} not found");

            items.Add(new EstimateItem
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Quantity = i.Quantity,
                UnitPrice = product.SellingPrice
            });
        }

        var estimate = new Estimate
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            ReferenceNumber = request.ReferenceNumber,
            EstimateDateUtc = DateTime.UtcNow,
            ExpiryDateUtc = request.ExpiryDateUtc,
            Status = EstimateStatus.Draft,
            Items = items
        };

        _db.Estimates.Add(estimate);
        await _db.SaveChangesAsync(ct);

        return new EstimateDto(
            estimate.Id,
            estimate.CustomerId,
            estimate.ReferenceNumber,
            estimate.EstimateDateUtc,
            estimate.Status,
            estimate.ExpiryDateUtc);
    }
}