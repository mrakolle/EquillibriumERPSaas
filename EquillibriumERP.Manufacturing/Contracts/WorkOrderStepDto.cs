using EquillibriumERP.Manufacturing.Domain.Enums;
public record WorkOrderStepDto(
    Guid Id,
    int StepNumber,
    string Description,
    TimeSpan? PlannedDuration,
    TimeSpan? ActualDuration,
    string? Material,
    decimal? ExpectedQuantity,
    decimal? ConsumedQuantity,
    string? UnitOfMeasure,
    StepStatus Status
);