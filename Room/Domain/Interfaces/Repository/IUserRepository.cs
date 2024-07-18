using Domain.Models;

namespace Domain.Interfaces.Repository;

public interface IUserRepository
{
    Task<User> Get(Guid id, CancellationToken token = default);
    Task<bool> Delete(Guid id, CancellationToken token = default);
}