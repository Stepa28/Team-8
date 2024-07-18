namespace Domain.Interfaces.Repository;

public interface IRepository<T> where T : class
{
    Task<T> Get(int id, CancellationToken token = default);
    Task<List<T>> GetList(CancellationToken token = default);
    Task<bool> Update(T entity, CancellationToken token = default);
    Task<bool> Delete(int id, CancellationToken token = default);
    Task Create(T entity, CancellationToken token = default);
}