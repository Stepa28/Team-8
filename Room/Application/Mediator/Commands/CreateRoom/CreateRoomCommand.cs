using Domain.Interfaces;
using Domain.Interfaces.Producers;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Enums;
using Team_8.Contracts.MassTransitDto;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Commands.CreateRoom;

public sealed record CreateRoomCommand(CreateRoomModel Model) : IRequest<RoomId>;

internal sealed class CreateRoomCommandHandler(
    ILogger<CreateRoomCommandHandler> logger,
    IRepository<Room> repository,
    IUserContext userContext,
    IUserRepository repositoryUser,
    IAddOrUpdateRoomProducer producer)
    : IRequestHandler<CreateRoomCommand, RoomId>
{
    public async Task<RoomId> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        //TODO хешировать пароль
        var user = await repositoryUser.GetAsync(userContext.User.Id, cancellationToken);
        var room = new Room { Title = request.Model.Title, HashPass = request.Model.Pass, RoomStatus = RoomStatus.Created, Creator = user };
        var res = await repository.CreateAsync(room, cancellationToken);

        logger.LogInformation("Пользователь с Id {@userId} созда комнату с Id {@roomId}", userContext.User.Id, res);

        var roomDto = new AddOrUpdateRoomDto(res, room.Title, room.RoomStatus, "", 0, userContext.User.Nick, RoomUpdateType.Add);
        await producer.PushAddOrUpdateRoom(roomDto, stoppingToken: cancellationToken);
        return new RoomId { Id = res };
    }
}