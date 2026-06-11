using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EquillibriumERP.Core.Identity.Application.Services;

public class RoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleService(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<ApplicationRole> CreateRoleAsync(string name, string? description = null)
    {
        var role = new ApplicationRole
        {
            Name = name,
            Description = description
        };

        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
            throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));

        return role;
    }

    public async Task<bool> RoleExistsAsync(string name)
    {
        return await _roleManager.RoleExistsAsync(name);
    }
}