namespace Application.Mediator.Commands.ConnectRoom;

public record ConnectRoomCommandModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string NameCurrentMap { get; set; }
    public int CurrentRound { get; set; }
    public string CreatorName { get; set; }
}