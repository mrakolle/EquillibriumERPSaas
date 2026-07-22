using EquillibriumERP.Manufacturing.Contracts;
using EquillibriumERP.Manufacturing.Domain.Entities;

namespace EquillibriumERP.Manufacturing.Interfaces;
public interface IBomService
{
    Task<Guid> CreateAsync(
        CreateBomRequest request, CancellationToken ct);

    Task<List<BomDto>> GetAllAsync(
        CancellationToken ct);

    Task<BomDto?> GetByIdAsync(
        Guid id, CancellationToken ct);

    Task<Guid> AddStepAsync(
        Guid bomId,
        CreateBOMStepRequest request,
        CancellationToken ct);

    Task<Guid> AddStepMaterialAsync(
        Guid stepId,
        CreateBOMStepMaterialRequest request,
        CancellationToken ct);

    Task<Guid> RecordConsumptionAsync(
        Guid stepId,
        RecordStepConsumptionRequest request,
        CancellationToken ct);

    Task<List<StepExpectedMaterialDto>> GetStepExpectedAsync(
        Guid stepId, CancellationToken ct);

    Task<StepVarianceDto> GetStepVarianceAsync(
        Guid stepId, CancellationToken ct);

    Task UpdateAsync(
        Guid id,
        UpdateBomRequest request,
        CancellationToken ct);
}