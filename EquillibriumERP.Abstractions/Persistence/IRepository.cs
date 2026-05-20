namespace EquillibriumERP.Abstractions.Persistence;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken ct = default);

    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task<List<T>> FindAsync(
        Func<IQueryable<T>, IQueryable<T>> query,
        CancellationToken ct = default);

    Task AddAsync(T entity, CancellationToken ct = default);

    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default);

    void Update(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}