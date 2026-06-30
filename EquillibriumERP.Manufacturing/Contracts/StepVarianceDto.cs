namespace EquillibriumERP.Manufacturing.Contracts;
public class StepVarianceDto
{
    public Guid RawMaterialProductId { get; set; }
    public decimal Expected { get; set; }
    public decimal Actual { get; set; }
    public decimal Variance => Actual - Expected;
}