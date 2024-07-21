using Domain.Common.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.Enums;

namespace Application.Mediator.Commands.BattleStatusUpdate;

public sealed record BattleStatusUpdate(BattleStatusDto Model) : IRequest;

internal sealed class BattleStatusUpdateHandler(ILogger<BattleStatusUpdateHandler> logger, IRepository<Room> repository) : IRequestHandler<BattleStatusUpdate>
{
    public async Task Handle(BattleStatusUpdate request, CancellationToken cancellationToken)
    {
        var room = await repository.GetAsync(request.Model.Id, cancellationToken);
        if(room == null)
            throw new NotFoundException($"Комната с Id = {request.Model.Id} не найдена");
        
        room.CurrentRound = request.Model.RoundCurrent;
        room.RoomStatus = request.Model.State == BattleState.Finish ? RoomStatus.Completed : RoomStatus.Created;
        await repository.SaveChangedAsync(cancellationToken);
    }
}