using Team_8.Contracts.Enums;

namespace Team_8.Contracts.MassTransitDto;

public record AddOrUpdateRoomDto(int Id, string Title, RoomStatus Status, string NameCurrentMap, int CurrentRound, string CreatorName, RoomUpdateType Type);