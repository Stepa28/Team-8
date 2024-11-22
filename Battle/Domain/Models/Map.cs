using Domain.Common;
using Team_8.Contracts.Enums;

namespace Domain.Models;

public record Map(List<TilesType> TilesType, int CountRow, int CountColumn) : BaseEntitySoftDelete<Map>
{
    public virtual List<Battle> Battle { get; set; }

    public override void Update(Map entity)
    {
        Battle = entity.Battle;
    }
}