using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Abstractions.Persistence;

public interface ITenantDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}