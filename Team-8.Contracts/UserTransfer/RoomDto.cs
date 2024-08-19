using Team_8.Contracts.Enums;

namespace Team_8.Contracts.UserTransfer;

public sealed record RoomDto(int Id, string Title, RoomStatus Status, string NameCurrentMap, int CurrentRound, string CreatorName) : IUserTransferDto;