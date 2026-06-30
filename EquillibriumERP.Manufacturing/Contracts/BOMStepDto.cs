using EquillibriumERP.Manufacturing.Domain.Enums;
namespace EquillibriumERP.Manufacturing.Contracts;
public class BOMStepDto
{
    public int StepNumber { get; set; }

    public string Description { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }

    public StepType Type { get; set; }

    public List<BOMStepMaterialDto> Materials { get; set; } = new();
}