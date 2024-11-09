using Domain.Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Protos.CodeFirst;

namespace Application.Mediator.Commands.ConnectBattle;

internal sealed class ConnectBattleCommandHandler(
    IRepository<Battle> battleRepository,
    IUserContext context,
    ILogger<ConnectBattleCommandHandler> logger
) : IRequestHandler<ConnectBattleCommand, CurrentMapStatusModel>
{
    public async Task<CurrentMapStatusModel> Handle(ConnectBattleCommand request, CancellationToken cancellationToken)
    {
        var battle = await battleRepository.GetAsync(request.Model.Id, cancellationToken);
        if(battle is null)
            throw new NotFoundException("Сражение не найдено");
        if(!battle.CurrentUnitStates.Select(x => x.UserId).Contains(context.User.Id))
            throw new ForbiddenAccessException("Вас нет в этом сражении");

        var result = new CurrentMapStatusModel
        {
            Tileses = battle.Map.MapToTilesModel(), Units = battle.CurrentUnitStates.Select(x => x.MapToUnitModel()).ToList()
        };

        return result;
    }
}