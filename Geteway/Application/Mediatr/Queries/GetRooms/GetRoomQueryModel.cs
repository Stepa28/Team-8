namespace Application.Mediatr.Queries.GetRooms;

public sealed record GetRoomQueryModel(int Id, string Title, int Status, string NameCurrentMap, int CurrentRound, string CreatorName);