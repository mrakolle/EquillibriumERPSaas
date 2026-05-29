namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class Feature
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }
}