using Team_8.Contracts.Enums;

namespace Team_8.Contracts.UserTransfer;

public record TilesDto(List<TilesType> TilesType, int CountRow, int CountColumn) : IUserTransferDto;