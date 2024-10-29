using Domain.Common;
using Team_8.Contracts.DTOs;

namespace Domain.Models;

public record Battle : BaseEntitySoftDelete<Battle>
{
    public virtual TilesDto Map { get; init; }
    public List<Guid> Users { get; init; }
    public Guid WalkingPlayer { get; set; }
    public virtual List<CurrentUnitState> CurrentUnitStates { get; init; } = [];

    public override void Update(Battle entity)
    {
        WalkingPlayer = entity.WalkingPlayer;
    }
}