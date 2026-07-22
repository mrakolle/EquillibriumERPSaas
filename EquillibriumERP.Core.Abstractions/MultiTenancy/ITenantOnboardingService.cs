namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface ITenantOnboardingService
{
    Task<TenantOnboardingResult> OnboardTenantAsync(
    CreateTenantRequest request,
    CancellationToken ct);
}