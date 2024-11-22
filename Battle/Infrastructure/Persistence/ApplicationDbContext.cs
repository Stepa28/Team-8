using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Team_8.Contracts.DTOs;

namespace Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies().UseNpgsql();
    }

    #region Tables

    public DbSet<Battle> Battles => Set<Battle>();
    public DbSet<Map> Maps => Set<Map>();
    public DbSet<CurrentUnitState> CurrentUnitStates => Set<CurrentUnitState>();

    public DbSet<T>? GetTable<T>() where T : class
    {
        var type = typeof(T);

        if(type == typeof(Battle))
            return Battles as DbSet<T>;
        if(type == typeof(Map))
            return Maps as DbSet<T>;
        if(type == typeof(CurrentUnitState))
            return CurrentUnitStates as DbSet<T>;
        return null;
    }

    #endregion
}