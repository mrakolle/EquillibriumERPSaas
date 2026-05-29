namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class AuditLog
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid? UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string Data { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}