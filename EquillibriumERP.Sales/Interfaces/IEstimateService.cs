using EquillibriumERP.Sales.Contracts.Estimates;

namespace EquillibriumERP.Sales.Interfaces;
public interface IEstimateService
{
    Task<EstimateDto?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<EstimateDto> CreateEstimateAsync(CreateEstimateRequest request, CancellationToken ct);
}