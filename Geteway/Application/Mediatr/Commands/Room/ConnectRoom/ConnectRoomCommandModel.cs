namespace Application.Mediatr.Commands.Room.ConnectRoom;

public sealed record ConnectRoomCommandModel(int Id, string Title, int Status, string NameCurrentMap, int CurrentRound, string CreatorName);