namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface ITenantModuleSeeder
{
    int Order { get; }

    string Name { get; }

    Task SeedAsync(
        string? schema = null,
        CancellationToken ct = default);
}