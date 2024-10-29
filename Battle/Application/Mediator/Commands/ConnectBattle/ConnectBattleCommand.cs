using MediatR;
using Team_8.Contracts.Protos.CodeFirst;

namespace Application.Mediator.Commands.ConnectBattle;

public sealed record ConnectBattleCommand(ConnectBattleModel Model) : IRequest<CurrentMapStatusModel>;