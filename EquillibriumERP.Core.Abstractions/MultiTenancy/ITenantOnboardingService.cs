namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface ITenantOnboardingService
{
    Task<Guid> OnboardTenantAsync(
        string tenantName,
        CancellationToken cancellationToken = default);
}