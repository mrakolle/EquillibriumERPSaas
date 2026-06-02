namespace EquillibriumERP.ControlPlane.Contracts.Responses;

public sealed record CreateRoleRequest(
    string Name,
    List<string> Permissions);