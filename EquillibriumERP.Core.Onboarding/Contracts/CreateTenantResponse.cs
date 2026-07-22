

using System;

namespace EquillibriumERP.Core.Onboarding.Contracts;
public sealed class CreateTenantResponse
{
    public Guid TenantId { get; init; }

    public string Schema { get; init; } = string.Empty;

    public string WorkspaceUrl { get; init; } = string.Empty;
}