using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Enums;

namespace Application.Mediator.Commands;

internal sealed class CreateBattleCommandHandler(
    IRepository<Battle> battleRepository,
    IRepository<CurrentUnitState> currentUnitStateRepository,
    ILogger<CreateBattleCommandHandler> logger
) : IRequestHandler<CreateBattleCommand>
{
    public async Task Handle(CreateBattleCommand request, CancellationToken cancellationToken)
    {
        var battle = request.Dto.MapToBattle();
        battle.WalkingPlayer = request
            .Dto
            .Units
            .First()
            .UserId;
        battle.State = BattleState.InProgress;

        await battleRepository.CreateAsync(battle, cancellationToken);

        var currentUnitState = request
            .Dto
            .Units
            .Select((t, i) => t.MapToCurrentUnitState() with { Battle = battle, Index = i })
            .ToList();

        foreach (var entry in currentUnitState)
            await currentUnitStateRepository.CreateAsync(entry, cancellationToken);

        logger.LogInformation("Битва создана с ID: {battleId}", battle.Id);
    }
}