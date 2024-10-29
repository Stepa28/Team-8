using Team_8.Contracts.DTOs;
using Team_8.Contracts.Enums;

namespace Team_8.Contracts.MassTransitDto;

public record BattleStateDto(List<CurrentUnitDto> UnitList, TilesDto Tiles, int RoundCurrent, BattleState State);