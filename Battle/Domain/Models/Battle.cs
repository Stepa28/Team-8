using Domain.Common;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.Enums;

namespace Domain.Models;

public record Battle : BaseEntitySoftDelete<Battle>
{
    public virtual TilesDto Map { get; init; }
    public List<Guid> Users { get; init; }
    public Guid WalkingPlayer { get; set; }
    public int Round { get; set; }
    public BattleState State { get; set; } = BattleState.None;
    public virtual List<CurrentUnitState> CurrentUnitStates { get; init; } = [];

    public override void Update(Battle entity)
    {
        WalkingPlayer = entity.WalkingPlayer;
    }
}