namespace Domain.Interfaces.Repository;

public interface IRepository<T> where T : class
{
    Task<T> GetAsync(int id, CancellationToken token = default);
    Task<List<T>> GetListAsync(CancellationToken token = default);
    Task<bool> UpdateAsync(T entity, CancellationToken token = default);
    Task<bool> DeleteAsync(int id, CancellationToken token = default);
    Task CreateAsync(T entity, CancellationToken token = default);
}