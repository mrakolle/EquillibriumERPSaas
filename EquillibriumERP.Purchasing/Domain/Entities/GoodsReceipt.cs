namespace EquillibriumERP.Purchasing.Domain.Entities;

public class GoodsReceipt
{
    public Guid Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public Guid PurchaseOrderId { get; set; }

    public DateTime ReceivedDateUtc { get; set; }
    public Guid PurchaseOrderLineId { get; set; }

    public string Status { get; set; } = "Draft";

    public PurchaseOrder PurchaseOrder { get; set; } = null!;

    public ICollection<GoodsReceiptLine> Lines { get; set; }
        = new List<GoodsReceiptLine>();
}