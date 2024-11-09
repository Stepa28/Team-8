using MediatR;
using Team_8.Contracts.MassTransitDto;

namespace Application.Mediatr.Commands.Battle.SendBattleState;

public sealed record SendBattleStateCommand(BattleStateDto Dto) : IRequest;