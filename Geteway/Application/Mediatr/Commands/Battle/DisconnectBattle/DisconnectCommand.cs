using MediatR;

namespace Application.Mediatr.Commands.Battle.DisconnectBattle;

public sealed record DisconnectCommand(int Id) : IRequest;