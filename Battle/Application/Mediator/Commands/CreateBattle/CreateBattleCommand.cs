using MediatR;
using Team_8.Contracts.MassTransitDto;

namespace Application.Mediator.Commands.CreateBattle;

public sealed record CreateBattleCommand(CreateBattleDto Dto) : IRequest;