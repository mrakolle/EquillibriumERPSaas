namespace EquillibriumERP.Identity.Application.Requests;

public sealed record CreateRoleRequest(
    string Name,
    List<string> Permissions);