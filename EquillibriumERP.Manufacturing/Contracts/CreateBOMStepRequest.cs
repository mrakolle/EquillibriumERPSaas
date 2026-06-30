using EquillibriumERP.Manufacturing.Domain.Enums;

namespace EquillibriumERP.Manufacturing.Contracts;
public class CreateBOMStepRequest
{
    public int StepNumber { get; set; }

    public string Description { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }

    public StepType Type { get; set; }

    public Guid? RawMaterialProductId { get; set; }

    public decimal QuantityPercentage { get; set; }

}