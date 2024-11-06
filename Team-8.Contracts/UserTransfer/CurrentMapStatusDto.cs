using Team_8.Contracts.Enums;

namespace Team_8.Contracts.UserTransfer;

public record CurrentMapStatusDto(int BattleId, List<UnitDto> Units, TilesDto Tileses , BattleState Status) : IUserTransferDto;

