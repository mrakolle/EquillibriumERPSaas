
namespace EquillibriumERP.Manufacturing.Domain.Entities;
public class BOMStepMaterial
{
    public Guid Id { get; set; }

    public Guid BOMStepId { get; set; }

    public decimal Quantity { get; set; }

    public Guid RawMaterialProductId { get; set; }
}