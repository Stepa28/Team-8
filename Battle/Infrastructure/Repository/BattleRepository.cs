using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class BattleRepository(IApplicationDbContext context) : BaseRepository<Battle>(context)
{
    public override async Task<Battle?> GetAsync(int id, CancellationToken token = default)
    {
        var entity = await context
            .Battles
            .Include(x => x.CurrentUnitStates)
            .SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == id, token);
        return entity;
    }
};