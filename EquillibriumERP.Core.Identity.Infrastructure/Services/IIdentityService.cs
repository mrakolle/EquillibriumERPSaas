using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Infrastructure.Persistence;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;
using EquillibriumERP.Core.Identity.Infrastructure.Entities;
namespace EquillibriumERP.Core.Identity.Infrastructure.Services;

public interface IIdentityService
{
    Task<User?> ValidateTenantUserAsync(string email, string password);
}