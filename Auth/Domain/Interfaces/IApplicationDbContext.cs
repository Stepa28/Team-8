using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<T>? GetTable<T>() where T : class;
}