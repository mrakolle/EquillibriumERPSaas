using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EquillibriumERP.Core.Infrastructure.Persistence;

public class TenantModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        if (context is TenantDbContext tenantDbContext)
        {
            return (context.GetType(), tenantDbContext.Schema, designTime);
        }

        return (context.GetType(), designTime);
    }
}