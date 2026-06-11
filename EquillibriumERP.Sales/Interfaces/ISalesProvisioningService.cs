using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquillibriumERP.Sales.Application.DTOs;
using EquillibriumERP.Sales.Application.Contracts.Estimates;
using EquillibriumERP.Sales.Application.Contracts.Invoices;
using EquillibriumERP.Sales.Application.Contracts.Purchases;


namespace EquillibriumERP.Sales.Application.Interfaces;

public interface ISalesProvisioningService
{
    Task<Guid> CreateEstimateAsync(
        CreateEstimateRequest request,
        CancellationToken ct);

    Task<Guid> CreateInvoiceAsync(
        CreateInvoiceRequest request,
        CancellationToken ct);
}





