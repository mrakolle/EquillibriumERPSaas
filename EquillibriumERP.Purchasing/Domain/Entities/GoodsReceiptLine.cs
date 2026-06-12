namespace EquillibriumERP.Purchasing.Domain.Entities;

public class GoodsReceiptLine
{
    public Guid Id { get; set; }

    public Guid GoodsReceiptId { get; set; }

    public Guid ProductId { get; set; }

    public Guid PurchaseOrderLineId { get; set; }

    public decimal QuantityReceived { get; set; }

    public string? SupplierLotNo { get; set; }

    public GoodsReceipt GoodsReceipt { get; set; } = null!;
}