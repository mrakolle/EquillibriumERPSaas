namespace EquillibriumERP.ControlPlane.Contracts.Requests;

public sealed record UpdateRoleRequest(
    string Name,
    string Description,
    IReadOnlyList<string> Permissions
);