using Team_8.Contracts.Enums;

namespace Team_8.Contracts.UserTransfer;

public record BattleStateUpdateDto(
    int BattleId,
    List<CurrentUnitDto> UnitList,
    TilesDto Tiles,
    int RoundCurrent,
    BattleState State,
    Guid WalkingPlayer) : IUserTransferDto;