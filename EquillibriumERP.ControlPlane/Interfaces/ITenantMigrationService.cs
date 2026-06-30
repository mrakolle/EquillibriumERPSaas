namespace EquillibriumERP.ControlPlane.Interfaces;

public interface ITenantMigrationService
{
    Task UpdateTenantSchemasAsync(CancellationToken ct);
}