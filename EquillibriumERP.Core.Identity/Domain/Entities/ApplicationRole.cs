using Microsoft.AspNetCore.Identity;

namespace EquillibriumERP.Core.Identity.Domain.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public string? Description { get; set; }
}