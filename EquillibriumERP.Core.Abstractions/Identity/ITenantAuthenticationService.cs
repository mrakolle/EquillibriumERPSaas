
namespace EquillibriumERP.Core.Abstractions.Identity;

public interface ITenantAuthenticationService
{
    Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);
}