namespace EquillibriumERP.Core.Identity.Application.Responses;
public record LoginResponse(
        string Token,
        Guid UserId,
        string TenantCode
    );