using System.Linq.Expressions;
using Domain.Common;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public class BaseRepository<T>(IApplicationDbContext context) : IRepository<T> where T : BaseEntitySoftDelete<T>
{
    private DbSet<T> Table => context.GetTable<T>() ?? throw new InvalidOperationException();
    public virtual async Task SaveChangedAsync(CancellationToken token = default) => await context.SaveChangesAsync(token);

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>>? where = null, CancellationToken token = default)
    {
        var entity = await Table.Where(where ?? (_ => true)).SingleOrDefaultAsync(token);
        return entity;
    }

    public virtual async Task<T?> GetAsync(int id, CancellationToken token = default)
    {
        var entity = await Table.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == id, token);
        return entity;
    }

    public virtual async Task<List<T>> GetListAsync(CancellationToken token = default, Expression<Func<T, bool>>? where = null)
    {
        var entity = await Table.Where(where ?? (_ => true)).ToListAsync(token);
        return entity;
    }

    public virtual async Task<bool> UpdateAsync(T entity, CancellationToken token = default)
    {
        var entityTmp = await Table.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == entity.Id, token);
        if(entityTmp == null || entityTmp.Id == 0)
            return false;

        entityTmp.Update(entity);

        await SaveChangedAsync(token);
        return true;
    }

    public virtual async Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        var entity = await Table.SingleOrDefaultAsync(x => x.Id == id, token);
        if(entity == null || entity.Id == 0 || entity.IsDeleted)
            return false;
        entity.IsDeleted = true;
        await SaveChangedAsync(token);
        return true;
    }

    public virtual async Task<int> CreateAsync(T entity, CancellationToken token = default)
    {
        var res = await Table.AddAsync(entity, token);
        await SaveChangedAsync(token);
        return res.Entity.Id;
    }
}