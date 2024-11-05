using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    #region Tables

    public DbSet<User> Users => Set<User>();

    public DbSet<T>? GetTable<T>() where T : class
    {
        var type = typeof(T);

        if(type == typeof(User))
            return Users as DbSet<T>;

        return null;
    }

    #endregion
}