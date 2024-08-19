using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public sealed record RoomInfoDto(int Id, string Title, RoomStatus Status, string NameCurrentMap, int CurrentRound, string CreatorName);