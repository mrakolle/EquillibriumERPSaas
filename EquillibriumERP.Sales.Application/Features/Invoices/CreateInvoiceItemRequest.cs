namespace EquillibriumERP.Sales.Application.Features.Invoices;
public class CreateInvoiceItemRequest
{
    public Guid ProductId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TaxRate { get; set; }
}