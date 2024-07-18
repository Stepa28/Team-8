using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Map> Maps { get; }
    DbSet<Room> Rooms { get; }
    DbSet<UnitType> UnitTypes { get; }
    DbSet<UserState> UserStates { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<T>? GetTable<T>() where T : class;
}