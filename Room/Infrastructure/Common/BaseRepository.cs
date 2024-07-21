using Domain.Common;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public class BaseRepository<T>(IApplicationDbContext context) : IRepository<T> where T : BaseEntitySoftDelete<T>
{
    private DbSet<T> Table => context.GetTable<T>() ?? throw new InvalidOperationException();
    
    public async Task<T> GetAsync(int id, CancellationToken token = default)
    {
        var entity = await Table.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == id, token);
        return entity;
    }

    public async Task<List<T>> GetListAsync(CancellationToken token = default)
    {
        var entity = await Table.ToListAsync(token);
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken token = default)
    {
        var entityTmp = await Table.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == entity.Id, token);
        if(entityTmp == null || entityTmp.Id == 0)
            return false;

        entityTmp.Update(entity);

        await context.SaveChangesAsync(token);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        var entity = await Table.SingleOrDefaultAsync(x => x.Id == id, token);
        if(entity == null || entity.Id == 0 || entity.IsDeleted)
            return false;
        entity.IsDeleted = true;
        await context.SaveChangesAsync(token);
        return true;
    }

    public async Task CreateAsync(T entity, CancellationToken token = default)
    {
        await Table.AddAsync(entity, token);
        await context.SaveChangesAsync(token);
    }
}