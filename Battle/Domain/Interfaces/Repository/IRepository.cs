using System.Linq.Expressions;

namespace Domain.Interfaces.Repository;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(Expression<Func<T, bool>>? where = null, CancellationToken token = default);
    Task<T?> GetAsync(int id, CancellationToken token = default);
    Task<List<T>> GetListAsync(CancellationToken token = default, Expression<Func<T, bool>>? where = null);
    Task<bool> UpdateAsync(T entity, CancellationToken token = default);
    Task<bool> DeleteAsync(int id, CancellationToken token = default);
    Task<int> CreateAsync(T entity, CancellationToken token = default);
    Task SaveChangedAsync(CancellationToken token = default);
}