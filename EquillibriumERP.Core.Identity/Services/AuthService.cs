using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EquillibriumERP.Core.Identity.Domain.Entities;
//using EquillibriumERP.Core.Identity.Application.Requests;
using EquillibriumERP.Core.Identity.Application.Responses;
using EquillibriumERP.Core.Identity.Application.Requests;
using EquillibriumERP.Core.Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using EquillibriumERP.Core.Identity.Auth;

namespace EquillibriumERP.Core.Identity.Application.Services;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IdentityDbContext _db;
    private readonly JwtOptions _jwt;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IdentityDbContext db,
        IOptions<JwtOptions> jwt)
    {
        _userManager = userManager;
        _db = db;
        _jwt = jwt.Value;
    }
    public async Task<AuthResult> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new Exception("Invalid credentials");

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!passwordValid)
            throw new Exception("Invalid credentials");

        var roles = (await _userManager.GetRolesAsync(user)).ToList();

        // tenant resolution (from user or lookup via tenant code later)
        var tenantId = user.TenantId;

        var permissions = await (
            from rp in _db.RolePermissions
            join r in _db.Roles on rp.RoleId equals r.Id
            join p in _db.Permissions on rp.PermissionId equals p.Id
            where roles.Contains(r.Name!)
            select p.Name
        )
        .Distinct()
        .ToListAsync();

        var token = GenerateToken(user, tenantId, roles, permissions);

        return new AuthResult
        {
            AccessToken = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes),
            UserId = user.Id,
            TenantId = tenantId,
            Roles = roles,
            Permissions = permissions
        };
    }
    private string GenerateToken(
    ApplicationUser user,
    Guid tenantId,
    List<string> roles,
    List<string> permissions)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("tenantId", tenantId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "")
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        claims.AddRange(permissions.Select(p => new Claim("permission", p)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}