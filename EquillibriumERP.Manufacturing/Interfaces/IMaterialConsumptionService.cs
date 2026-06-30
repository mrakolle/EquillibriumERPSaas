using EquillibriumERP.Manufacturing.Contracts;

namespace EquillibriumERP.Manufacturing.Interfaces;
public interface IMaterialConsumptionService
{
    Task ConsumeAsync(MaterialConsumptionRequest request, CancellationToken ct);
}