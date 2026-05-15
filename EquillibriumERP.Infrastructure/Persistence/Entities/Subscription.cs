namespace EquillibriumERP.Infrastructure.Persistence.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string PlanName { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}