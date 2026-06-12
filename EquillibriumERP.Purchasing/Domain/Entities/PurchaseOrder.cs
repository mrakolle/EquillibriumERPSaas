
using EquillibriumERP.Purchasing.Domain.Enums;

namespace EquillibriumERP.Purchasing.Domain.Entities;

public class PurchaseOrder
{
    public Guid Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public Guid SupplierId { get; set; }

    public DateTime OrderDateUtc { get; set; }

    public PurchaseOrderStatus Status { get; set; } = PurchaseOrderStatus.Draft;

    public Supplier Supplier { get; set; } = null!;

    public ICollection<PurchaseOrderLine> Lines { get; set; }
        = new List<PurchaseOrderLine>();
}