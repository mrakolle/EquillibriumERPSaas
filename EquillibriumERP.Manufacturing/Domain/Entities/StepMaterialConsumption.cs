namespace EquillibriumERP.Manufacturing.Domain.Entities;
public class StepMaterialConsumption
{
    public Guid Id { get; set; }

    public Guid WorkOrderStepId { get; set; }   // FIXED LINK

    public Guid RawMaterialProductId { get; set; }

    public decimal QuantityUsed { get; set; }

    public DateTime RecordedAt { get; set; }
}