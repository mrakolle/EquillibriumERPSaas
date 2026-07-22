using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Application.Contracts.Invoices;
using EquillibriumERP.Sales.Application.Contracts.Purchases;


namespace EquillibriumERP.Sales.Interfaces;

public interface ISalesService
{
    Task<Guid> CreateEstimateAsync(
        CreateEstimateRequest request,
        CancellationToken ct);

    Task<Guid> CreateInvoiceAsync(
        CreateInvoiceRequest request,
        CancellationToken ct);
}





