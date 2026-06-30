namespace EquillibriumERP.Manufacturing.Contracts;
public record WorkOrderMaterialDto(
    Guid Id,
    Guid RawMaterialProductId,
    decimal PlannedQuantity,
    string UnitOfMeasure
);