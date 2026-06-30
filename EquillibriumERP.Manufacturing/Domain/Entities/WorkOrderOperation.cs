namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class WorkOrderOperation
{
    public Guid Id { get; set; }

    public Guid WorkOrderId { get; set; }

    public int StepNumber { get; set; }

    public string OperationName { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    public List<OperationMaterial> Materials { get; set; } = new();
}