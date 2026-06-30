using EquillibriumERP.Manufacturing.Domain.Enums;

namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class BOMStep
{
    public Guid Id { get; set; }
    public Guid BillOfMaterialId { get; set; }
    public int StepNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public StepType Type { get; set; }
    
    // NEW: Material consumed by this step (optional)
    public Guid? RawMaterialProductId { get; set; }

    // NEW: Percentage of the material consumed during this step
    public decimal QuantityPercentage { get; set; }

    public StepStatus Status { get; set; } = StepStatus.Pending;
}