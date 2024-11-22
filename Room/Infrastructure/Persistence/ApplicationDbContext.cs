using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Team_8.Contracts.Enums;

namespace Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
        if(Maps.Any(x => x.Name == "4x4")) return;
        Maps.AddRange([
            new Map
            {
                Id = 1,
                Name = "4x4",
                CountRow = 4,
                CountColumn = 4,
                TilesType =
                [
                    TilesType.Grass, TilesType.Grass, TilesType.Wall, TilesType.Grass,
                    TilesType.Wall, TilesType.Grass, TilesType.Grass, TilesType.Grass,
                    TilesType.Grass, TilesType.Grass, TilesType.Grass, TilesType.Wall,
                    TilesType.Grass, TilesType.Wall, TilesType.Grass, TilesType.Grass
                ]
            },
            new Map
            {
                Id = 2,
                Name = "3x3CentralWall",
                CountRow = 3,
                CountColumn = 3,
                TilesType =
                [
                    TilesType.Grass, TilesType.Grass, TilesType.Grass,
                    TilesType.Grass, TilesType.Wall, TilesType.Grass,
                    TilesType.Grass, TilesType.Grass, TilesType.Grass
                ]
            },
            new Map
            {
                Id = 3,
                Name = "2x2",
                CountRow = 2,
                CountColumn = 2,
                TilesType =
                [
                    TilesType.Grass, TilesType.Grass, TilesType.Grass, TilesType.Grass
                ]
            }
        ]);
        SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies().UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    #region Tables

    public DbSet<User> Users => Set<User>();
    public DbSet<Map> Maps => Set<Map>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<UnitType> UnitTypes => Set<UnitType>();
    public DbSet<UserState> UserStates => Set<UserState>();

    public DbSet<T>? GetTable<T>() where T : class
    {
        var type = typeof(T);

        if(type == typeof(User))
            return Users as DbSet<T>;
        if(type == typeof(Map))
            return Maps as DbSet<T>;
        if(type == typeof(Room))
            return Rooms as DbSet<T>;
        if(type == typeof(UnitType))
            return UnitTypes as DbSet<T>;
        if(type == typeof(UserState))
            return UserStates as DbSet<T>;

        return null;
    }

    #endregion
}