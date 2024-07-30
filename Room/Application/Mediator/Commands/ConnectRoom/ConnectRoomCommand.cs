using Application.Common.Attributes;
using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Producers;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.Enums;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Commands.ConnectRoom;

[Authorize(Role.User)]
public sealed record ConnectRoomCommand(ConnectRoomModel Model) : IRequest<RoomModel>;

internal sealed class ConnectRoomCommandHandler(
    ILogger<ConnectRoomCommandHandler> logger
    , IUserContext userContext
    , IUserJoinLeaveProducer producer
    , IRepository<UserState> repositoryState
    , IUserRepository repositoryUser
    , IRepository<Room> repositoryRoom
    , IMapper mapper)
    : IRequestHandler<ConnectRoomCommand, RoomModel>
{
    public async Task<RoomModel> Handle(ConnectRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await repositoryRoom.GetAsync(request.Model.Id, cancellationToken);
        if(room == null)
            throw new NotFoundException($"Комнаты с Id({request.Model.Id}) не существует");
        var user = await repositoryUser.GetAsync(userContext.User.Id, cancellationToken);
        if(user == null)
            throw new NotFoundException("Вас нет в этой БД");

        //TODO нормально проверять пароль
        if(room.HashPass != request.Model.Pass)
            throw new ForbiddenAccessException("Пароль неверен");

        var state = new UserState { User = user, Room = room };
        await repositoryState.CreateAsync(state, cancellationToken);

        var entity = mapper.Map<UserJoinLeaveDto>(userContext.User);
        entity.RoomId = request.Model.Id;
        entity.State = UserJoinLeave.Join;
        await producer.PushUserJoinLeave(entity, cancellationToken);

        var res = mapper.Map<RoomModel>(mapper.Map<CreateRoomModel>(room));
        return res;
    }
}