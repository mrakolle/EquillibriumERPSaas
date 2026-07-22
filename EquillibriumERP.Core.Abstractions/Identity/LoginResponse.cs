namespace EquillibriumERP.Core.Abstractions.Identity;

public sealed class LoginResponse
{
    public Guid UserId { get; init; }

    public Guid TenantId { get; init; }

    public string TenantName { get; init; } = string.Empty;

    public string UserName { get; init; } = string.Empty;

    public string AccessToken { get; init; } = string.Empty;
}