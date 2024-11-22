namespace Team_8.Contracts.UserTransfer;

public sealed record ChoseMapDto(int RoomId, int MapId) : IUserTransferDto;