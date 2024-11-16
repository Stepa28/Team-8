using Domain.Models;
using Riok.Mapperly.Abstractions;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.Protos.CodeFirst;

namespace Application.Mediator.Commands.ConnectBattle;

[Mapper]
public static partial class ConnectBattleCommandMapping
{
    [MapProperty(nameof(CurrentUnitState.XCord), nameof(UnitModel.X))]
    [MapProperty(nameof(CurrentUnitState.YCord), nameof(UnitModel.Y))]
    [MapProperty(nameof(CurrentUnitState.UserId), nameof(UnitModel.PlayerId))]
    [MapProperty(nameof(CurrentUnitState.UnitTypeId), nameof(UnitModel.UnitType))]

    public static partial UnitModel MapToUnitModel(this CurrentUnitState query);
    
    
    public static partial TilesModel MapToTilesModel(this Map query);
}