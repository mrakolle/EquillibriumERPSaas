namespace EquillibriumERP.Core.Abstractions.Authorization;

public sealed record PermissionDefinition(
    string Code,
    string Name,
    string Description);