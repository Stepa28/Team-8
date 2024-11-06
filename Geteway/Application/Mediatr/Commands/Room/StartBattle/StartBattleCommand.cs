using MediatR;

namespace Application.Mediatr.Commands.Room.StartBattle;

public sealed record StartBattleCommand(int Id) : IRequest;