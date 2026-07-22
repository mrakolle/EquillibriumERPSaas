using EquillibriumERP.Manufacturing.Contracts;
using EquillibriumERP.Manufacturing.Domain.Entities;

namespace EquillibriumERP.Manufacturing.Interfaces;

public interface IWorkOrderService
{
    Task<List<WorkOrderDto>> GetAllAsync(
        CancellationToken ct = default);

    Task<WorkOrderDto?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default);

    Task<Guid> CreateWorkOrderAsync(
        CreateWorkOrderRequest request,
        CancellationToken ct = default);

    Task StartWorkOrderAsync(
        Guid workOrderId,
        CancellationToken ct = default);

    Task StartStepAsync(
        Guid workOrderId,
        Guid workOrderStepId,
        CancellationToken ct = default);

    Task ExecuteStepAsync(
        Guid workOrderId,
        Guid workOrderStepId,
        ExecuteStepRequest request,
        CancellationToken ct = default);
}