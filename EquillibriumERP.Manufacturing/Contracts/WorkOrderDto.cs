using EquillibriumERP.Manufacturing.Domain.Enums;

namespace EquillibriumERP.Manufacturing.Contracts;
public record WorkOrderDto(
    Guid Id,
    string WorkOrderNumber,
    Guid BillOfMaterialId,
    string ProductName,
    decimal PlannedQuantity,
    string UnitOfMeasure,
    WorkOrderStatus Status,
    List<WorkOrderMaterialDto> Materials,
    List<WorkOrderStepDto> Steps
);