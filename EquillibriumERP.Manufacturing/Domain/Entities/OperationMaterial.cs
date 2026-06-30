namespace EquillibriumERP.Manufacturing.Domain.Entities;
public class OperationMaterial
{
    public Guid Id { get; set; }

    public Guid WorkOrderOperationId { get; set; }

    public Guid RawMaterialProductId { get; set; }

    public decimal Quantity { get; set; }

    public string UnitOfMeasure { get; set; } = string.Empty;
}