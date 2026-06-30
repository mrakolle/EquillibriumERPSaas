using EquillibriumERP.Manufacturing.Domain.Enums;

namespace EquillibriumERP.Manufacturing.Domain.Entities;
public class WorkOrderStep
{
    public Guid Id { get; set; }

    public Guid WorkOrderId { get; set; }
    public WorkOrder WorkOrder { get; set; } = null!;

    public Guid BOMProcessStepId { get; set; }

    public Guid? WorkOrderMaterialId { get; set; }
    public WorkOrderMaterial? WorkOrderMaterial { get; set; }

    public int StepNumber { get; set; }
    public string Action { get; set; } = string.Empty;

    public StepStatus Status { get; set; } = StepStatus.Pending;

    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

}