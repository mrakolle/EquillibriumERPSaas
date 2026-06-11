namespace EquillibriumERP.Sales.Domain.Entities;

public class InvoicePayment
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? Reference { get; set; }

    public string? Method { get; set; } // Cash, EFT, Card, etc.
}