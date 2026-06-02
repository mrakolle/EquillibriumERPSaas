using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;
using EquillibriumERP.Core.Identity.Infrastructure.Entities;

namespace EquillibriumERP.Core.Identity.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly TenantDbContext _db;

    public IdentityService(TenantDbContext db)
    {
        _db = db;
    }

    public async Task<MasterUser?> ValidateTenantUserAsync(string email, string password)
    {
        var user = await _db.Set<MasterUser>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user is null)
            return null;

        var hasher = new PasswordHasher<MasterUser>();

        var result = hasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            password
        );

        return result == PasswordVerificationResult.Success
            ? user
            : null;
    }

    Task<User?> IIdentityService.ValidateTenantUserAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}