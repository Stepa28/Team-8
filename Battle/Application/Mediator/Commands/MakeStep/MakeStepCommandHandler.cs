using Domain.Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Producers;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Enums;

namespace Application.Mediator.Commands.MakeStep;

internal sealed class MakeStepCommandHandler(
    IRepository<Battle> battleRepository,
    IUserContext context,
    IRepository<CurrentUnitState> currentUnitStateRepository,
    IBattleStateProducer stateProducer,
    IBattleStatusProducer statusProducer,
    ILogger<MakeStepCommandHandler> logger)
    : IRequestHandler<MakeStepCommand>
{
    public async Task Handle(MakeStepCommand request, CancellationToken cancellationToken)
    {
        var battle = await battleRepository.GetAsync(request.Model.BattleId, cancellationToken);
        if(battle is null)
            throw new NotFoundException("Сражение не найдено");
        if(battle.State == BattleState.Finish)
            throw new BadRequestException("Сражение завершено");
        if(battle.WalkingPlayer != context.User.Id)
            throw new BadRequestException("Сейчас не ваша очередь ходить");

        var unit = await currentUnitStateRepository
            .GetAsync(x =>
                    x.BattleId == request.Model.BattleId
                    && x.UserId == context.User.Id,
                cancellationToken);
        if(unit == null)
            throw new ForbiddenAccessException("Вас нет в этом сражении");

        unit.Move(request.Model.Move);

        switch (request.Model.Attack)
        {
            case Routes.Up:
            {
                var targetUnit = battle.CurrentUnitStates.FirstOrDefault(x =>
                    x.XCord == unit.XCord && x.YCord == unit.YCord - 1);
                if(targetUnit != null)
                    unit.Attack(targetUnit);
                break;
            }
            case Routes.Left:
            {
                var targetUnit = battle.CurrentUnitStates.FirstOrDefault(x =>
                    x.XCord == unit.XCord - 1 && x.YCord == unit.YCord);
                if(targetUnit != null)
                    unit.Attack(targetUnit);
                break;
            }
            case Routes.Right:
            {
                var targetUnit = battle.CurrentUnitStates.FirstOrDefault(x =>
                    x.XCord == unit.XCord + 1 && x.YCord == unit.YCord);
                if(targetUnit != null)
                    unit.Attack(targetUnit);
                break;
            }
            case Routes.Down:
            {
                var targetUnit = battle.CurrentUnitStates.FirstOrDefault(x =>
                    x.XCord == unit.XCord && x.YCord == unit.YCord + 1);
                if(targetUnit != null)
                    unit.Attack(targetUnit);
                break;
            }
            case Routes.None:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        await currentUnitStateRepository.SaveChangedAsync(cancellationToken);

        battle.Round++;
        var nextPlayer = battle.CurrentUnitStates[(unit.Index + 1) % battle.CurrentUnitStates.Count].UserId;
        battle.WalkingPlayer = nextPlayer;

        if(battle.CurrentUnitStates.Any(x => x.Health <= 0))
            battle.State = BattleState.Finish;

        await battleRepository.UpdateAsync(battle, cancellationToken);

        await stateProducer.PushUpdateBattleState(battle.MapToBattleStateDto(), cancellationToken);
        await statusProducer.PushUpdateBattleStatus(battle.MapToBattleStatusDto(), cancellationToken);
    }
}