using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Infrastructure.Auth;
using EquillibriumERP.Core.Identity.Infrastructure.Entities;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;

namespace EquillibriumERP.ControlPlane.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth")
            .WithTags("Authentication");

        group.MapPost("/login", HandleLogin)
            .AllowAnonymous()
            .WithName("Login");
    }

    private static async Task<IResult> HandleLogin(
        LoginRequest request,
        MasterDbContext masterDb,
        TenantDbContext tenantDb,
        ITenantResolver tenantResolver,
        JwtTokenService jwt)
    {
        if (string.IsNullOrWhiteSpace(request.TenantCode))
            return Results.BadRequest("TenantCode is required");

        // =========================
        // 1. SYSADMIN PATH
        // =========================

        if (request.TenantCode.Equals("sysadmin", StringComparison.OrdinalIgnoreCase))
        {
            var masterUser = await masterDb.MasterUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (masterUser is null)
                return Results.Unauthorized();

            var passwordHasher = new PasswordHasher<MasterUser>();

            var passwordResult = passwordHasher.VerifyHashedPassword(
                masterUser,
                masterUser.PasswordHash,
                request.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
                return Results.Unauthorized();

            var sysAdminToken = jwt.CreateToken(
                masterUser.Id,
                Guid.Empty,
                masterUser.Email);

            return Results.Ok(new LoginResponse(
                sysAdminToken,
                masterUser.Id,
                "sysadmin"));
        }

        // =========================
        // 2. TENANT PATH
        // =========================

        var tenant = await masterDb.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Code == request.TenantCode);

        if (tenant is null)
            return Results.Unauthorized();

        tenantResolver.SetTenant(tenant.Schema);

        tenantDb.EnsureTenantSchema();

        var tenantUser = await tenantDb.Set<MasterUser>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (tenantUser is null)
            return Results.Unauthorized();

        var tenantPasswordHasher = new PasswordHasher<MasterUser>();

        var tenantPasswordResult = tenantPasswordHasher.VerifyHashedPassword(
            tenantUser,
            tenantUser.PasswordHash,
            request.Password);

        if (tenantPasswordResult == PasswordVerificationResult.Failed)
            return Results.Unauthorized();

        var tenantToken = jwt.CreateToken(
            tenantUser.Id,
            tenant.Id,
            tenantUser.Email);

        return Results.Ok(new LoginResponse(
            tenantToken,
            tenantUser.Id,
            request.TenantCode));
    }

    public record LoginRequest(
        string Email,
        string Password,
        string TenantCode
    );

    public record LoginResponse(
        string Token,
        Guid UserId,
        string TenantCode
    );
}