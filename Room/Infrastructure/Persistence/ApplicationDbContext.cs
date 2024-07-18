using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

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

    #endregion

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
}