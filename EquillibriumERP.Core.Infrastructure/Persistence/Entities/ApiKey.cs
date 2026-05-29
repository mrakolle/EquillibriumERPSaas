namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class ApiKey
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Key { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime ExpiresAtUtc { get; set; }
}