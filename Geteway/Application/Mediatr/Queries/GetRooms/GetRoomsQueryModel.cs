namespace Application.Mediatr.Queries.GetRooms;

public sealed record GetRoomsQueryModel(int Id, string Title, int Status, string NameCurrentMap, int CurrentRound, string CreatorName);