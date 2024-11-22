namespace Team_8.Contracts.UserTransfer;

public record CurrentUnitDto(Guid UserId, int UnitTypeId, int X, int Y, byte Hp) : IUserTransferDto;