namespace EquillibriumERP.Sales.Application.Contracts.Invoices;

public sealed record CreateInvoiceRequest(
    Guid CustomerId);