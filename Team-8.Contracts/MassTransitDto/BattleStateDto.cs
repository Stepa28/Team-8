using Team_8.Contracts.Enums;
using Team_8.Contracts.UserTransfer;
using TilesDto = Team_8.Contracts.DTOs.TilesDto;

namespace Team_8.Contracts.MassTransitDto;

public record BattleStateDto(int BattleId, List<CurrentUnitDto> UnitList, TilesDto Tiles, int RoundCurrent, BattleState State, Guid WalkingPlayer);