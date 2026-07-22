using EquillibriumERP.Sales.Contracts;

namespace EquillibriumERP.Sales.Interfaces;

public interface IEstimateService
{
    Task<EstimateResponse> CreateAsync(
        CreateEstimateRequest request,
        CancellationToken cancellationToken = default);

    Task<EstimateResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<EstimateSummaryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<EstimateResponse> UpdateAsync(
        Guid id,
        UpdateEstimateRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<EstimateResponse> SubmitAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<EstimateResponse> AcceptAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<EstimateResponse> RejectAsync(
        Guid id,
        CancellationToken cancellationToken = default);

}