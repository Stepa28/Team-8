namespace Team_8.Contracts.UserTransfer;

public sealed record CreateRoomDto(string Title, string Password) : IUserTransferDto;