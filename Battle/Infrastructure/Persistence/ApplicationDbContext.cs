using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.Enums;

namespace Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies().UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TilesDto>().HasKey(x => x.TilesType);
        base.OnModelCreating(modelBuilder);
    }

    #region Tables

    public DbSet<Battle> Battles => Set<Battle>();
    public DbSet<CurrentUnitState> CurrentUnitStates => Set<CurrentUnitState>();

    public DbSet<T>? GetTable<T>() where T : class
    {
        var type = typeof(T);

        if(type == typeof(Battle))
            return Battles as DbSet<T>;
        if(type == typeof(CurrentUnitState))
            return CurrentUnitStates as DbSet<T>;
        return null;
    }

    #endregion
}