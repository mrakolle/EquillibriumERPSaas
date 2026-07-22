using EquillibriumERP.Core.Abstractions.Identity;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Identity.Auth;
using EquillibriumERP.Core.Identity.Infrastructure;
using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Core.Identity.Services;

public sealed class TenantAuthenticationService
    : ITenantAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITenantLookup _tenantLookup;
    private readonly JwtTokenService _jwtTokenService;
    private readonly IdentityDbContext _identityDbContext;
    private readonly ITenantSession _tenantSession;
    private readonly ITenantContextualizer _tenantContextualizer;

    public TenantAuthenticationService(
    UserManager<ApplicationUser> userManager,
    ITenantLookup tenantLookup,
    JwtTokenService jwtTokenService,
    IdentityDbContext identityDbContext,
    ITenantSession tenantSession,
    ITenantContextualizer tenantContextualizer)
    {
        _userManager = userManager;
        _tenantLookup = tenantLookup;
        _jwtTokenService = jwtTokenService;
        _identityDbContext = identityDbContext;
        _tenantSession = tenantSession;
        _tenantContextualizer = tenantContextualizer;
    }

   public async Task<LoginResponse> LoginAsync(
    LoginRequest request,
    CancellationToken ct = default)
    {
        // ==========================================================
        // STEP 1: LOOK UP TENANT IN public.Tenants
        // ==========================================================
        
        var tenant = await _tenantLookup.GetByCodeAsync(request.TenantCode, ct);
        
        if(tenant is null)
            throw new InvalidOperationException("Invalid tenant");

        // ==========================================================
        // STEP 2: SET TENANT CONTEXT
        // ==========================================================
        _tenantSession.SetTenant(tenant.Id, tenant.Schema);

        await _tenantContextualizer
            .SetTenantContextAsync(_identityDbContext, ct);


        // ==========================================================
        // STEP 3: USER LOOKUP (SCHEMA RESOLVED BY INFRASTRUCTURE)
        // ==========================================================
    
        var user = await _userManager.Users
            .FirstOrDefaultAsync(
                x => x.Email == request.Email,
                ct);

        if (user is null || !user.IsActive)
            throw new InvalidOperationException("Invalid credentials");

        // ==========================================================
        // STEP 4: PASSWORD VALIDATION
        // ==========================================================
        var passwordValid = await _userManager
            .CheckPasswordAsync(user, request.Password);

        if (!passwordValid)
            throw new InvalidOperationException("Invalid credentials");

        // ==========================================================
        // STEP 5: TOKEN
        // ==========================================================
        var token = _jwtTokenService.CreateToken(
            user,
            tenant.Id.ToString(),
            tenant.Code,
            tenant.Schema);

        // ==========================================================
        // STEP 6: RESPONSE
        // ==========================================================
        return new LoginResponse
        {
            UserId = user.Id,
            TenantId = tenant.Id,
            TenantName = tenant.Name,
            UserName = $"{user.FirstName} {user.LastName}".Trim(),
            AccessToken = token
        };
    }
}