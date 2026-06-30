namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface IRawMaterialSeeder
{
    Task SeedAsync(
    string? schema,
    CancellationToken ct);
}