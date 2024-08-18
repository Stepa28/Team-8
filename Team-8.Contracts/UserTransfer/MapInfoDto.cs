namespace Team_8.Contracts.UserTransfer;

public sealed record MapInfoDto(int Id, string Name, int Row, int Column) : IUserTransferDto;