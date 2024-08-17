using MediatR;

namespace Application.Mediatr.Commands.StartBattle;

public sealed record StartBattleCommand(int Id) : IRequest;