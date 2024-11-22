using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Interfaces.Producers;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Enums;
using Team_8.Contracts.MassTransitDto;

namespace Application.Mediator.Commands.BattleStatusUpdate;

public sealed record BattleStatusUpdateCommand(BattleStatusDto Model) : IRequest;

internal sealed class BattleStatusUpdateCommandHandler(
    ILogger<BattleStatusUpdateCommandHandler> logger,
    IRepository<Room> repository,
    IRepository<Map> mapRepository,
    IAddOrUpdateRoomProducer producer,
    IMapper mapper) : IRequestHandler<BattleStatusUpdateCommand>
{
    public async Task Handle(BattleStatusUpdateCommand request, CancellationToken cancellationToken)
    {
        var room = await repository.GetAsync(request.Model.Id, cancellationToken);
        if(room == null)
            throw new NotFoundException($"Комната с Id = {request.Model.Id} не найдена");

        room.CurrentRound = request.Model.RoundCurrent;
        room.RoomStatus = request.Model.State == BattleState.Finish ? RoomStatus.Completed : RoomStatus.InBattle;
        await repository.SaveChangedAsync(cancellationToken);
        
        var map = await mapRepository.GetAsync(x => !x.IsDeleted && x.Id == room.CurrentMapId, cancellationToken);
        
        var roomDto = new AddOrUpdateRoomDto(room.Id, room.Title, room.RoomStatus, map.Name, 0, "",
            RoomUpdateType.Status | RoomUpdateType.CurrentRound);
        await producer.PushAddOrUpdateRoom(roomDto, cancellationToken);
    }
}