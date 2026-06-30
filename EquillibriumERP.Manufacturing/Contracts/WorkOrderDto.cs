using EquillibriumERP.Manufacturing.Domain.Enums;

namespace EquillibriumERP.Manufacturing.Contracts;

public record WorkOrderDto(
    Guid Id,
    string WorkOrderNumber,
    Guid BillOfMaterialId,
    decimal PlannedQuantity,
    WorkOrderStatus Status,
    List<WorkOrderMaterialDto> Materials
);