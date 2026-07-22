using System;


namespace EquillibriumERP.Core.Onboarding.Contracts;
public sealed class CreateTenantAdministratorRequest
{
    public Guid TenantId { get; init; }

    public string Schema { get; init; } = string.Empty;

    public string AdministratorName { get; init; } = string.Empty;

    public string EmailAddress { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}