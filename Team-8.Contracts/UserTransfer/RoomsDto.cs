namespace Team_8.Contracts.UserTransfer;

public sealed record RoomsDto(List<RoomDto> Rooms) : IUserTransferDto;