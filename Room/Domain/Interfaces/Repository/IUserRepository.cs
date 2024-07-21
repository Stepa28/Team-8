using Domain.Models;

namespace Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task CreateAsync(Guid id, CancellationToken token = default);
    Task<User?> GetAsync(Guid id, CancellationToken token = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
}