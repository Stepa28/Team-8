using MediatR;

namespace Application.Mediatr.Commands.Battle.ConnectBattle;

public sealed record ConnectCommand(int Id) : IRequest<ConnectCommandModel>;