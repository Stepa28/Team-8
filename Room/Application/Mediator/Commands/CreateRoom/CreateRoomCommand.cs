using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Enums;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Commands.CreateRoom;

public sealed record CreateRoomCommand(CreateRoomModel Model) : IRequest<RoomId>;

internal sealed class CreateRoomCommandHandler(ILogger<CreateRoomCommandHandler> logger, IRepository<Room> repository)
    : IRequestHandler<CreateRoomCommand, RoomId>
{
    public async Task<RoomId> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        //TODO хешировать пароль
        var res = await repository.CreateAsync(
            new Room { Title = request.Model.Title, HashPass = request.Model.Pass, RoomStatus = RoomStatus.Created }, cancellationToken);
        return new RoomId { Id = res };
    }
}