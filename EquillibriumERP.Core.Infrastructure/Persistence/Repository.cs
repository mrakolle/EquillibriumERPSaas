using EquillibriumERP.Core.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Core.Infrastructure.Persistence;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly TenantDbContext _db;
    private readonly DbSet<T> _set;

    public Repository(TenantDbContext db)
    {
        _db = db;
        _set = db.Set<T>();
    }

    // READ
    public async Task<List<T>> GetAllAsync(CancellationToken ct = default)
        => await _set.ToListAsync(ct);

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _set.FindAsync(new object[] { id }, ct);

    public async Task<List<T>> FindAsync(
        Func<IQueryable<T>, IQueryable<T>> query,
        CancellationToken ct = default)
    {
        var q = query(_set.AsQueryable());
        return await q.ToListAsync(ct);
    }

    // WRITE
    public async Task AddAsync(T entity, CancellationToken ct = default)
        => await _set.AddAsync(entity, ct);

    public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
        => await _set.AddRangeAsync(entities, ct);

    public void Update(T entity)
        => _set.Update(entity);

    public void Remove(T entity)
        => _set.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities)
        => _set.RemoveRange(entities);

    // UNIT OF WORK
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}