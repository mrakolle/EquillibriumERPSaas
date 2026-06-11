using EquillibriumERP.Sales.Domain.Enums;

namespace EquillibriumERP.Sales.Domain.Entities;

public class Invoice
{
    public Guid Id { get; private set; }

    public string InvoiceNumber { get; private set; } = null!;

    public Guid CustomerId { get; private set; }

    public Guid? EstimateId { get; private set; }

    public DateTime InvoiceDateUtc { get; private set; }

    public DateTime DueDateUtc { get; private set; }

    public InvoiceStatus Status { get; private set; }

    public decimal Subtotal { get; private set; }

    public decimal TaxAmount { get; private set; }

    public decimal TotalAmount { get; private set; }

    public decimal PaidAmount { get; private set; }

    public string Notes { get; private set; } = string.Empty;

    public ICollection<InvoiceItem> Items { get; private set; }
        = new List<InvoiceItem>();

    private Invoice() { }

    public Invoice(
        string invoiceNumber,
        Guid customerId,
        DateTime invoiceDateUtc,
        DateTime dueDateUtc,
        Guid? estimateId = null,
        string? notes = null)
    {
        Id = Guid.NewGuid();

        InvoiceNumber = invoiceNumber;

        CustomerId = customerId;

        InvoiceDateUtc = invoiceDateUtc;

        DueDateUtc = dueDateUtc;

        EstimateId = estimateId;

        Status = InvoiceStatus.Draft;

        Notes = notes ?? string.Empty;
    }

    public void AddItem(InvoiceItem item)
    {
        Items.Add(item);

        RecalculateTotals();
    }

    public void RecordPayment(decimal amount)
    {
        PaidAmount += amount;

        if (PaidAmount <= 0)
        {
            Status = InvoiceStatus.Sent;
            return;
        }

        if (PaidAmount < TotalAmount)
        {
            Status = InvoiceStatus.PartiallyPaid;
            return;
        }

        Status = InvoiceStatus.Paid;
    }

    private void RecalculateTotals()
    {
        Subtotal = Items.Sum(x => x.LineSubtotal);

        TaxAmount = Items.Sum(x => x.TaxAmount);

        TotalAmount = Subtotal + TaxAmount;
    }
}