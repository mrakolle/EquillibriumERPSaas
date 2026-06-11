using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Sales.Application.Contracts.Estimates;
using EquillibriumERP.Sales.Application.Contracts.Invoices;
using EquillibriumERP.Sales.Application.Interfaces;
//using EquillibriumERP.Products.;
//using EquillibriumERP.Products.Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace EquillibriumERP.Sales.Services;

public sealed class SalesProvisioningService
    : ISalesProvisioningService
{
    public Task<Guid> CreateInvoiceAsync(
        CreateInvoiceRequest request,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Guid.NewGuid());
    }

    public Task<Guid> CreateEstimateAsync(
        CreateEstimateRequest request,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}