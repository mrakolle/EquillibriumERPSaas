namespace EquillibriumERP.ControlPlane.Application.Contracts.Responses;
public sealed record RoleResponse(
    Guid Id,
    string Name,
    string Description,
    IReadOnlyList<string> Permissions);