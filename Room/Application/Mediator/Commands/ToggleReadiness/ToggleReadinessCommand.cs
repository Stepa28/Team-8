using Domain.Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Commands.ToggleReadiness;

public sealed record ToggleReadinessCommand(RoomId Model) : IRequest;

internal sealed class ToggleReadinessCommandHandler(
    ILogger<ToggleReadinessCommandHandler> logger
    , IRepository<UserState> repositoryUserState
    , IUserRepository repositoryUser
    , IUserContext userContext)
    : IRequestHandler<ToggleReadinessCommand>
{
    public async Task Handle(ToggleReadinessCommand request, CancellationToken cancellationToken)
    {
        var state = await repositoryUserState.GetAsync(x => !x.IsDeleted
                                                            && x.UserId.Equals(userContext.User.Id)
                                                            && x.RoomId == request.Model.Id
            , cancellationToken);
        if(state == null)
            throw new NotFoundException("Состояние не найдено");

        state.ReadyForBattle = !state.ReadyForBattle;
        await repositoryUserState.SaveChangedAsync(cancellationToken);
    }
}