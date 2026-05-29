namespace EquillibriumERP.ControlPlane.Onboarding;

public interface ITenantOnboardingService
{
    Task<Guid> OnboardTenantAsync(
        string tenantName,
        CancellationToken cancellationToken = default);
}