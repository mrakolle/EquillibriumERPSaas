namespace EquillibriumERP.Core.Abstractions.Identity;

public sealed class CreateUserRequest
{
    public Guid TenantId { get; init; }

    public string Email { get; init; } = string.Empty;

    public string UserName { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string? FirstName { get; init; }

    public string? LastName { get; init; }
}