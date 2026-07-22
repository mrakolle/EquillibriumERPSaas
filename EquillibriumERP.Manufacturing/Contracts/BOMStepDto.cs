using EquillibriumERP.Manufacturing.Domain.Enums;

namespace EquillibriumERP.Manufacturing.Contracts;
public class BOMStepDto
{
    public Guid Id { get; set; }

    public int StepNumber { get; set; }

    public string Description { get; set; } = string.Empty;

    public TimeSpan Duration { get; set; }

    public StepType Type { get; set; }

    public Guid? RawMaterialProductId { get; set; }

    public string? RawMaterialName { get; set; }

    public decimal QuantityPercentage { get; set; }
}