namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public static class TenantMapper
{
    public static Abstractions.MultiTenancy.Tenant ToDomain(this Tenant entity)
    {
        return new Abstractions.MultiTenancy.Tenant
        {
            Id = entity.Id,
            Name = entity.Name,
            Code = entity.Code,
            Schema = entity.Schema,
            IsActive = entity.IsActive
        };
    }
}