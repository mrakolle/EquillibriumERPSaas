namespace EquillibriumERP.Sales.Application.Features.Invoices;
public class CreateInvoiceRequest
{
    public Guid CustomerId { get; set; }
    public DateTime DueDateUtc { get; set; }
    public string Notes { get; set; } = string.Empty;

    public List<CreateInvoiceItemRequest> Items { get; set; } = new();
}