namespace EquillibriumERP.Infrastructure.Persistence.Entities;

public class Payment
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaidAtUtc { get; set; }

    public string Provider { get; set; } = string.Empty;

    public string TransactionReference { get; set; } = string.Empty;
}