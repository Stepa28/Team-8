using Team_8.Contracts.Enums;

namespace Team_8.Contracts.UserTransfer;

public record UpdateRoomDto(int Id, string Title, RoomStatus Status, string NameCurrentMap, int CurrentRound, string CreatorName, RoomUpdateType Type)
    : IUserTransferDto;