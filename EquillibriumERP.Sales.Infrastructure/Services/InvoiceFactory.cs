using EquillibriumERP.Sales.Domain.Entities;

namespace EquillibriumERP.Sales.Infrastructure.Services;

public static class InvoiceFactory
{
    public static Invoice CreateFromEstimate(
        Estimate estimate,
        string invoiceNumber)
    {
        var invoice = new Invoice(
            invoiceNumber,
            estimate.CustomerId,
            DateTime.UtcNow,
            estimate.ExpiryDateUtc.AddDays(30)
        );

        foreach (var item in estimate.Items)
        {
            invoice.AddItem(new InvoiceItem(
                item.ProductId,
                item.Description,
                item.Quantity,
                item.UnitPrice,
                item.TaxRate
            ));
        }

        return invoice;
    }
}