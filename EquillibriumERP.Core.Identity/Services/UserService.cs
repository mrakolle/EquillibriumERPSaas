using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace EquillibriumERP.Core.Identity.Application.Services;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> RegisterAsync(string email, string password, Guid tenantId)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            TenantId = tenantId,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));

        return user;
    }

    public async Task AssignRoleAsync(ApplicationUser user, string role)
    {
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}