using EquillibriumERP.Sales.Domain.Entities;

namespace EquillibriumERP.Sales.Services;

public static class InvoiceFactory
{
    public static Invoice CreateFromEstimate(
        Estimate estimate,
        string invoiceNumber)
    {
        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            CustomerId = estimate.CustomerId,
            InvoiceNumber = invoiceNumber,
            InvoiceDateUtc = DateTime.UtcNow,
            TaxAmount = 0m
        };

        foreach (var item in estimate.Items)
        {
            invoice.AddItem(new InvoiceItem
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoice.Id,
                ProductId = item.ProductId,
                Description = item.Description,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TaxRate = item.TaxRate
            });
        }

        return invoice;
    }
}