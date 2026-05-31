namespace EquillibriumERP.ControlPlane.Application.Contracts.Requests;

public sealed record UpdateRoleRequest(
    string Name,
    string Description,
    IReadOnlyList<string> Permissions);