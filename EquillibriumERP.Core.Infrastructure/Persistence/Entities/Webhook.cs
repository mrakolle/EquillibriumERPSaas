namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class Webhook
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Url { get; set; } = string.Empty;

    public string EventName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}