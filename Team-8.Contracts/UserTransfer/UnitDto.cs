namespace Team_8.Contracts.UserTransfer;

public record UnitDto(
    Guid PlayerId,
    int X,
    int Y,
    int Health,
    int UnitType) : IUserTransferDto;