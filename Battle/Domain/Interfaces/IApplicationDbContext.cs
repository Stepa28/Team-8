using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Battle> Battles { get; }
    DbSet<CurrentUnitState> CurrentUnitStates { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<T>? GetTable<T>() where T : class;
}