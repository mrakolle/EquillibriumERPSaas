namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class ProductBatch
{
    public Guid Id { get; set; }

    public Guid WorkOrderId { get; set; }

    public string BatchNumber { get; set; } = string.Empty;

    public Guid ProductId { get; set; }

    public decimal QuantityProduced { get; set; }

    public DateTime CreatedAt { get; set; }

    public WorkOrder WorkOrder { get; set; } = null!;
}