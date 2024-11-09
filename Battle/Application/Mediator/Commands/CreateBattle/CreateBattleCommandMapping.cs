using Domain.Models;
using Riok.Mapperly.Abstractions;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.MassTransitDto;

namespace Application.Mediator.Commands;

[Mapper]
public static partial class CreateBattleCommandMapping
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
}