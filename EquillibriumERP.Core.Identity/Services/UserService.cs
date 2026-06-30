using EquillibriumERP.Core.Abstractions.Identity;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Identity.Auth;
using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Core.Identity.Infrastructure.Services;

public sealed class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITenantResolver _tenantResolver;
    private readonly IdentityDbContext _identityDbContext;
    private readonly ITenantSession _tenantSession;
    private readonly ITenantLookup _tenantLookup;
    private readonly JwtTokenService _jwtTokenService;
    private readonly ITenantContextualizer _tenantContextualizer;

    public UserService(
    UserManager<ApplicationUser> userManager,
    ITenantLookup tenantLookup,
    ITenantResolver tenantResolver,
    IdentityDbContext identityDbContext,
    JwtTokenService jwtTokenService,
    ITenantContextualizer tenantContextualizer,
    ITenantSession tenantSession)
    {
        _userManager = userManager;
        _tenantLookup = tenantLookup;
        _tenantResolver = tenantResolver;
        _jwtTokenService = jwtTokenService;
        _tenantSession = tenantSession;
        _identityDbContext = identityDbContext;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken ct = default)
    {
        // 1. TENANT LOOKUP (PUBLIC SCHEMA via abstraction)
        var tenant = await _tenantLookup.GetByCodeAsync(request.TenantCode, ct);

        if (tenant is null)
            throw new InvalidOperationException("Invalid tenant");

        // 2. USER LOOKUP (TENANT SCOPED)
        Console.WriteLine("Tenant Code "+ tenant.Code + " verified & Schema is : " + tenant.Schema); 
        _tenantSession.SetTenant(
            tenant.Id,
            tenant.Schema);
            

        var user = await _userManager.Users
            .FirstOrDefaultAsync(x =>
                x.Email == request.Email &&
                x.TenantId == tenant.Id,
                ct);

        if (user is null || !user.IsActive)
            throw new InvalidOperationException("Invalid credentials");

        // 3. PASSWORD CHECK
        var passwordValid = await _userManager
            .CheckPasswordAsync(user, request.Password);

        if (!passwordValid)
            throw new InvalidOperationException("Invalid credentials");

        // 4. TOKEN
        var token = _jwtTokenService.CreateToken(
            user,
            tenant.Id.ToString(),
            tenant.Code,
            tenant.Schema);

        return new LoginResponse
        {
            UserId = user.Id,
            TenantId = user.TenantId,
            AccessToken = token
        };
    }

    public async Task<CreateUserResponse> CreateAsync(
        CreateUserRequest request,
        CancellationToken ct = default)
    {
        var tenantId =
        request.TenantId != Guid.Empty
            ? request.TenantId
            : throw new InvalidOperationException("TenantId is required");
        await _tenantContextualizer
            .SetTenantContextAsync(_identityDbContext, ct);
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            TenantId = tenantId,
            Email = request.Email,
            NormalizedEmail = request.Email.ToUpperInvariant(),
            UserName = request.UserName,
            NormalizedUserName = request.UserName.ToUpperInvariant(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            IsActive = true
        };
        await _tenantContextualizer
            .SetTenantContextAsync(_identityDbContext, ct);
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User creation failed: {errors}");
        }

        return new CreateUserResponse
        {
            UserId = user.Id,
            Email = user.Email!,
            UserName = user.UserName!
        };
    }
}