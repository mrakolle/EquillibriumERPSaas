namespace EquillibriumERP.ControlPlane.Application.Contracts.Requests;

public sealed record CreateRoleRequest(
    string Name,
    List<string> Permissions);