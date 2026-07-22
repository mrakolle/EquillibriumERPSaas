using EquillibriumERP.Core.Abstractions.Products;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Domain.Enums;
using EquillibriumERP.Sales.Domain.Entities;
using EquillibriumERP.Sales.Interfaces;
using EquillibriumERP.Sales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Sales.Services;

public class EstimateService : IEstimateService
{
    private readonly SalesDbContext _db;
    private readonly ITenantResolver _tenantResolver;
    private readonly ITenantSession _tenantSession;
    private readonly IProductLookup _productLookup;
    private readonly ITenantContextualizer _tenantContextualizer;

    public EstimateService(
        SalesDbContext db,
        ITenantResolver tenantResolver,
        ITenantContextualizer tenantContextualizer,
        ITenantSession tenantSession,
        IProductLookup productLookup)
    {
        _db = db;
        _tenantResolver = tenantResolver;
        _tenantContextualizer = tenantContextualizer;
        _tenantSession = tenantSession;
        _productLookup = productLookup;
    }

    public async Task<EstimateResponse> CreateAsync(
    CreateEstimateRequest request,
    CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);
    

        var customer = await GetCustomerAsync(
            request.CustomerId,
            ct);

        var estimate = new Estimate
        {
            Id = Guid.NewGuid(),

            QuoteNumber = await GenerateReferenceNumberAsync(ct),
            Reference = request.Reference,

            CustomerId = customer.Id,

            CustomerName = customer.Name,

            EstimateDateUtc = DateTime.UtcNow,

            ExpiryDateUtc = request.ExpiryDateUtc is null
            ? null
            : DateTime.SpecifyKind(
                request.ExpiryDateUtc.Value,
                DateTimeKind.Utc),

            Notes = request.Notes,

            Status = EstimateStatus.Draft,

            CreatedUtc = DateTime.UtcNow,
            ModifiedUtc = DateTime.UtcNow
        };


        foreach (var requestItem in request.Items)
        {
            var item = await BuildEstimateItemAsync(
                requestItem,
                ct);

            estimate.Items.Add(item);
        }


        CalculateTotals(estimate);


        _db.Estimates.Add(estimate);
        Console.WriteLine(
    $"EstimateDateUtc: {estimate.EstimateDateUtc.Kind}");

Console.WriteLine(
    $"ExpiryDateUtc: {estimate.ExpiryDateUtc?.Kind}");

Console.WriteLine(
    $"CreatedUtc: {estimate.CreatedUtc.Kind}");

Console.WriteLine(
    $"ModifiedUtc: {estimate.ModifiedUtc.Kind}");

        await _db.SaveChangesAsync(ct);


        return MapToResponse(estimate, customer);
    }

    private async Task<Customer> GetCustomerAsync(
    Guid customerId,
    CancellationToken ct)
    {
        var customer = await _db.Customers
            .FirstOrDefaultAsync(
                x => x.Id == customerId,
                ct);

        if (customer is null)
            throw new InvalidOperationException(
                "Customer not found.");

        return customer;
    }

    private void CalculateTotals(
    Estimate estimate)
    {
        estimate.Subtotal =
            estimate.Items.Sum(x => x.LineSubtotal);


        estimate.DiscountAmount =
            estimate.Items.Sum(x =>
                x.LineSubtotal *
                (x.DiscountPercent / 100));


        estimate.TaxAmount =
            estimate.Items.Sum(x =>
                x.TaxAmount);


        estimate.TotalAmount =
            estimate.Subtotal
            - estimate.DiscountAmount
            + estimate.TaxAmount;
    }

    private EstimateResponse MapToResponse(
    Estimate estimate,
    Customer customer)
    {
        return new EstimateResponse(
            estimate.Id,
            estimate.QuoteNumber,
            estimate.Reference,

            estimate.CustomerId,
            customer.CustomerCode,
            customer.Name,
            customer.Email,
            customer.Phone,
            customer.VatNumber,

            estimate.Status,

            estimate.EstimateDateUtc,
            estimate.ExpiryDateUtc,

            estimate.Notes,

            estimate.Subtotal,
            estimate.DiscountAmount,
            estimate.TaxAmount,
            estimate.TotalAmount,

            estimate.Items
                .Select(item => new EstimateItemResponse(
                    item.Id,
                    item.ProductId,
                    item.ProductCode,
                    item.ProductName,
                    item.Description,
                    item.UnitOfMeasure,
                    item.Quantity,
                    item.UnitPrice,
                    item.DiscountPercent,
                    item.TaxRate,
                    item.LineSubtotal,
                    item.TaxAmount,
                    item.LineTotal
                ))
                .ToList()
        );
    }

    private async Task<EstimateItem> BuildEstimateItemAsync(
        CreateEstimateItemRequest requestItem,
        CancellationToken ct)
    {
        var product = await _productLookup.GetByIdAsync(
            requestItem.ProductId,
            ct);

        if (product is null)
            throw new InvalidOperationException(
                $"Product '{requestItem.ProductId}' not found.");


        var lineSubtotal =
            requestItem.Quantity *
            requestItem.UnitPrice;


        var lineDiscount =
            lineSubtotal *
            (requestItem.DiscountPercent / 100);


        var taxableAmount =
            lineSubtotal - lineDiscount;


        var lineTax =
            taxableAmount *
            (requestItem.TaxRate / 100);


        return new EstimateItem
        {
            Id = Guid.NewGuid(),

            ProductId = product.Id,

            ProductCode = product.ProductCode,

            ProductName = product.Name,

            Description = product.Description,

            UnitOfMeasure = product.UnitOfMeasure,

            Quantity = requestItem.Quantity,

            UnitPrice = requestItem.UnitPrice,

            DiscountPercent = requestItem.DiscountPercent,

            TaxRate = requestItem.TaxRate,

            LineSubtotal = lineSubtotal,

            TaxAmount = lineTax,

            LineTotal = taxableAmount + lineTax
        };
    }

    public async Task<EstimateResponse?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(
            _db,
            cancellationToken);

        var estimate = await _db.Estimates
            .Include(x => x.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (estimate is null)
            return null;

        var customer = await _db.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == estimate.CustomerId,
                cancellationToken);

        if (customer is null)
            throw new InvalidOperationException("Customer not found.");

        return new EstimateResponse(
            estimate.Id,
            estimate.QuoteNumber,
            estimate.Reference,

            customer.Id,
            customer.CustomerCode,
            customer.Name,
            customer.Email,
            customer.Phone,
            customer.VatNumber,

            estimate.Status,
            estimate.EstimateDateUtc,
            estimate.ExpiryDateUtc,
            estimate.Notes,

            estimate.Subtotal,
            estimate.DiscountAmount,
            estimate.TaxAmount,
            estimate.TotalAmount,

            estimate.Items
                .Select(item => new EstimateItemResponse(
                    item.Id,
                    item.ProductId,
                    item.ProductCode,
                    item.ProductName,
                    item.Description,
                    item.UnitOfMeasure,
                    item.Quantity,
                    item.UnitPrice,
                    item.DiscountPercent,
                    item.TaxRate,
                    item.LineSubtotal,
                    item.TaxAmount,
                    item.LineTotal
                ))
                .ToList()
        );
    }

    public async Task<IReadOnlyList<EstimateSummaryResponse>> GetAllAsync(
    CancellationToken cancellationToken = default)
    {
         await _tenantContextualizer.SetTenantContextAsync(_db, cancellationToken);
        return await _db.Estimates
            .AsNoTracking()
            .OrderByDescending(x => x.EstimateDateUtc)
            .Select(x => new EstimateSummaryResponse(
                x.Id,
                x.QuoteNumber,
                x.CustomerName,
                x.Status,
                x.EstimateDateUtc,
                x.TotalAmount
            ))
            .ToListAsync(cancellationToken);
    }

    public async Task<EstimateResponse> UpdateAsync(
    Guid id,
    UpdateEstimateRequest request,
    CancellationToken cancellationToken = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(
            _db,
            cancellationToken);

        var estimate = await _db.Estimates
            .Include(x => x.Items)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        if (estimate is null)
            throw new InvalidOperationException("Quotation not found.");

        var customer = await GetCustomerAsync(
            request.CustomerId,
            cancellationToken);

        estimate.Reference = request.Reference;
        estimate.CustomerId = customer.Id;
        estimate.CustomerName = customer.Name;

        estimate.ExpiryDateUtc =
            request.ExpiryDateUtc is null
                ? null
                : DateTime.SpecifyKind(
                    request.ExpiryDateUtc.Value,
                    DateTimeKind.Utc);

        estimate.Notes = request.Notes;
        estimate.ModifiedUtc = DateTime.UtcNow;

        // Remove existing lines
        _db.EstimateItems.RemoveRange(estimate.Items);

        // Add new lines
        foreach (var requestItem in request.Items)
        {
            var item = await BuildEstimateItemAsync(
                requestItem,
                cancellationToken);

            item.Id = Guid.NewGuid();
            item.EstimateId = estimate.Id;

            _db.EstimateItems.Add(item);
        }

        // Calculate totals from the request
        estimate.Subtotal = request.Items.Sum(x => x.Quantity * x.UnitPrice);

        estimate.DiscountAmount = request.Items.Sum(x =>
            (x.Quantity * x.UnitPrice) * (x.DiscountPercent / 100));

        estimate.TaxAmount = request.Items.Sum(x =>
        {
            var subtotal = x.Quantity * x.UnitPrice;
            var discount = subtotal * (x.DiscountPercent / 100);
            var taxable = subtotal - discount;

            return taxable * (x.TaxRate / 100);
        });

        estimate.TotalAmount =
            estimate.Subtotal
            - estimate.DiscountAmount
            + estimate.TaxAmount;

        await _db.SaveChangesAsync(cancellationToken);

        return MapToResponse(
            estimate,
            customer);
    }
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<EstimateResponse> SubmitAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<EstimateResponse> AcceptAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<EstimateResponse> RejectAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    private async Task<string> GenerateReferenceNumberAsync(CancellationToken ct)
    {
        var year = DateTime.UtcNow.Year;

        var prefix = $"QT-{year}-";

        var lastQuote = await _db.Estimates
            .Where(x => x.QuoteNumber.StartsWith(prefix))
            .OrderByDescending(x => x.QuoteNumber)
            .FirstOrDefaultAsync(ct);

        int nextNumber = 1;

        if (lastQuote != null)
        {
            var numberPart = lastQuote.QuoteNumber.Substring(prefix.Length);

            if (int.TryParse(numberPart, out var current))
                nextNumber = current + 1;
        }

        return $"{prefix}{nextNumber:D6}";
    }
}