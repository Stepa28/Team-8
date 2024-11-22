using Domain.Common;
using Team_8.Contracts.Enums;

namespace Domain.Models;

public record CurrentUnitState : BaseEntitySoftDelete<CurrentUnitState>
{
    public Guid UserId { get; set; }
    public UltimateType Ultimate { get; init; }
    public byte AttackPower { get; init; }
    public byte Health { get; set; }
    public int BattleId { get; init; }
    public int DefensePower { get; init; }
    public int Index { get; init; }
    public int Speed { get; init; }
    public int UnitTypeId { get; set; }
    public int XCord { get; set; }
    public int YCord { get; set; }
    public virtual Battle Battle { get; init; }
    
    public override void Update(CurrentUnitState entity)
    {
        Health = entity.Health;
        UserId = entity.UserId;
        UnitTypeId = entity.UnitTypeId;
        XCord = entity.XCord;
        YCord = entity.YCord;
    }
    
    public void Move(Routes move)
    {
        switch (move)
        {
            case Routes.Down or Routes.Up:
                YCord += Speed * (move == Routes.Down ? 1 : -1);
                break;
            case Routes.Left or Routes.Right:
                XCord += Speed * (move == Routes.Right? 1 : -1);
                break;
        }
    }

    public bool Attack(CurrentUnitState targetUnit)
    {
        targetUnit.Health -= AttackPower;
        return targetUnit.Health <= 0;
    }
}