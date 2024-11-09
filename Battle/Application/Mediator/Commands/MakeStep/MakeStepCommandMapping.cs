using Domain.Models;
using Riok.Mapperly.Abstractions;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.MassTransitDto;
using Team_8.Contracts.UserTransfer;

namespace Application.Mediator.Commands.MakeStep;

[Mapper]
public static partial class MakeStepCommandMapping
{
    [MapProperty(nameof(CreateBattleDto.Tiles), nameof(Battle.Map))]
    [MapProperty(nameof(CreateBattleDto.Units), nameof(Battle.Users), Use = nameof(MapUsers))]
    public static partial Battle MapToBattle(this CreateBattleDto query);
    
    [UserMapping(Default = false)]
    private static List<Guid> MapUsers(List<UnitTypeDto> units) => units.Select(x => x.UserId).ToList();

    [MapProperty(nameof(UnitTypeDto.Armor), nameof(CurrentUnitState.DefensePower))]
    [MapProperty(nameof(UnitTypeDto.Damage), nameof(CurrentUnitState.AttackPower))]
    [MapProperty(nameof(UnitTypeDto.HP), nameof(CurrentUnitState.Health))]
    [MapProperty(nameof(UnitTypeDto.MovementSpeed), nameof(CurrentUnitState.Speed))]
    public static partial CurrentUnitState MapToCurrentUnitState(this UnitTypeDto query);

    [MapProperty(nameof(Battle.Round), nameof(BattleStateDto.RoundCurrent))]
    [MapProperty(nameof(Battle.Id), nameof(BattleStateDto.BattleId))]
    public static partial BattleStatusDto MapToBattleStatusDto(this Battle query);
    
    [MapProperty(nameof(Battle.Round), nameof(BattleStateDto.RoundCurrent))]
    [MapProperty(nameof(Battle.Id), nameof(BattleStateDto.BattleId))]
    [MapProperty(nameof(Battle.Map), nameof(BattleStateDto.Tiles))]
    [MapProperty(nameof(Battle.CurrentUnitStates), nameof(BattleStateDto.UnitList), Use = nameof(MapUnits))]
    public static partial BattleStateDto MapToBattleStateDto(this Battle query);
    
    [UserMapping(Default = false)]
    private static List<CurrentUnitDto> MapUnits(List<CurrentUnitState> units) => units.Select(x => x.MapToCurrentUnitDto()).ToList();
    
    [MapProperty(nameof(CurrentUnitState.XCord), nameof(CurrentUnitDto.X))]
    [MapProperty(nameof(CurrentUnitState.YCord), nameof(CurrentUnitDto.Y))]
    [MapProperty(nameof(CurrentUnitState.Health), nameof(CurrentUnitDto.Hp))]
    private static partial CurrentUnitDto MapToCurrentUnitDto(this CurrentUnitState query);
}