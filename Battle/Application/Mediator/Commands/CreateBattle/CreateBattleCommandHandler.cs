using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

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
        
        await battleRepository.CreateAsync(battle, cancellationToken);

        var currentUnitState = request.Dto
            .Units
            .Select(x => x.MapToCurrentUnitState() with { Battle = battle })
            .ToList();

        foreach (var entry in currentUnitState)
            await currentUnitStateRepository.CreateAsync(entry, cancellationToken);

        logger.LogInformation("Битва создана с ID: {battleId}", battle.Id);
    }
}