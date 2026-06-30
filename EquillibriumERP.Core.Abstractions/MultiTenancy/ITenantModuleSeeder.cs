public interface ITenantModuleSeeder
{
    int Order { get; }

    Task SeedAsync(CancellationToken ct = default);
}