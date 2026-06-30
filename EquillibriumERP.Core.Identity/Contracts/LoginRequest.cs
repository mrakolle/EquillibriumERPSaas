namespace EquillibriumERP.Core.Identity.Application.Requests;

public record LoginRequest(
    string Email,
    string Password,
    string TenantCode
);