using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UserRepository(IApplicationDbContext context) : IUserRepository
{
    public async Task<User> Get(Guid id, CancellationToken token = default)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id.Equals(id), token);
        return user;
    }

    public async Task<bool> Delete(Guid id, CancellationToken token = default)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => x.Id.Equals(id), token);
        if(user == null || user.Id.Equals(Guid.Empty) || user.IsDeleted)
            return false;
        user.IsDeleted = true;
        await context.SaveChangesAsync(token);
        return true;
    }
}