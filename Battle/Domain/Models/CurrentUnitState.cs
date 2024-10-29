using Domain.Common;
using Team_8.Contracts.Enums;

namespace Domain.Models;

public record CurrentUnitState : BaseEntitySoftDelete<CurrentUnitState>
{
    public Guid UserId { get; set; }
    public UltimateType Ultimate { get; init; }
    public byte Health { get; set; }
    public int AttackPower { get; init; }
    public int DefensePower { get; init; }
    public int Speed { get; init; }
    public int UnitTypeId { get; set; }
    public int XCord { get; set; }
    public int YCord { get; set; }
    public int BattleId { get; init; }
    public virtual Battle Battle { get; init; }
    
    public override void Update(CurrentUnitState entity)
    {
        Health = entity.Health;
        UserId = entity.UserId;
        UnitTypeId = entity.UnitTypeId;
        XCord = entity.XCord;
        YCord = entity.YCord;
    }
}