using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EquillibriumERP.Core.Identity.Auth;

public sealed class JwtTokenService
{
    private readonly JwtOptions _options;

    public JwtTokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string CreateToken(ApplicationUser user, string tenantId, string tenantCode, string schema)
    {
        Console.WriteLine("=== JWT OPTIONS ===");
        Console.WriteLine($"Issuer: '{_options.Issuer}'");
        Console.WriteLine($"Audience: '{_options.Audience}'");
        Console.WriteLine($"SigningKey: '{_options.SigningKey}'");
        Console.WriteLine("=== END JWT OPTIONS ===");
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),

            new("tenant_id", tenantId),
            new("tenant_code", tenantCode),
            new("schema", schema),

            new("user_id", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.SigningKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}