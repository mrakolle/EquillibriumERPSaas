using Microsoft.AspNetCore.Identity;

namespace EquillibriumERP.Core.Identity.Domain.Entities
;

public class ApplicationUser : IdentityUser<Guid>
{
    public Guid TenantId { get; set; }
    public String? TenantCode { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public bool IsActive { get; set; } = true;
}