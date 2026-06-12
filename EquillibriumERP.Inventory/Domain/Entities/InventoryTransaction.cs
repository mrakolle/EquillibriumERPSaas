public class InventoryTransaction
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public decimal Quantity { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public string ReferenceType { get; set; } = string.Empty;

    public Guid ReferenceId { get; set; }

    public string? LotNo { get; set; }   

    public DateTime TransactionDateUtc { get; set; }
}