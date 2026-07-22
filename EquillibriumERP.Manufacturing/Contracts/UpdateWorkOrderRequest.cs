namespace EquillibriumERP.Manufacturing.Contracts;

public sealed class UpdateWorkOrderRequest
{
    public string? Notes { get; set; }
    public DateTime? PlannedStart { get; set; }
    public DateTime? PlannedEnd { get; set; }
}