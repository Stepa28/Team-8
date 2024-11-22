using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Commands.Room.CreateRoom;

internal sealed class CreateRoomCommandHandler(ILogger<CreateRoomCommandHandler> logger, IRoomService service) : IRequestHandler<CreateRoomCommand, CreateRoomCommandModel>
{
    public async Task<CreateRoomCommandModel> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var res = await service.CreateRoom(request.MapToCreateRoomModel(), cancellationToken);
        return res.MapToCreateRoomCommandModel();
    }
}