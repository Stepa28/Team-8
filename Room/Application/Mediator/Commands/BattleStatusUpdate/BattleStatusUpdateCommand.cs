using Domain.Common.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.Enums;

namespace Application.Mediator.Commands.BattleStatusUpdate;

public sealed record BattleStatusUpdateCommand(BattleStatusDto Model) : IRequest;

internal sealed class BattleStatusUpdateCommandHandler(ILogger<BattleStatusUpdateCommandHandler> logger, IRepository<Room> repository) : IRequestHandler<BattleStatusUpdateCommand>
{
    public async Task Handle(BattleStatusUpdateCommand request, CancellationToken cancellationToken)
    {
        var room = await repository.GetAsync(request.Model.Id, cancellationToken);
        if(room == null)
            throw new NotFoundException($"Комната с Id = {request.Model.Id} не найдена");
        
        room.CurrentRound = request.Model.RoundCurrent;
        room.RoomStatus = request.Model.State == BattleState.Finish ? RoomStatus.Completed : RoomStatus.Created;
        await repository.SaveChangedAsync(cancellationToken);
    }
}