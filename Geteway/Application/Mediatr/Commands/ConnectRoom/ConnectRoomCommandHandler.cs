using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Commands.ConnectRoom;

internal sealed class ConnectRoomCommandHandler(ILogger<ConnectRoomCommandHandler> logger, IRoomService service)
    : IRequestHandler<ConnectRoomCommand, ConnectRoomCommandModel>
{
    public async Task<ConnectRoomCommandModel> Handle(ConnectRoomCommand request, CancellationToken cancellationToken)
    {
        var res = await service.ConnectRoom(request.MapToConnectRoomModel(), cancellationToken);
        return res.MapToConnectRoomCommandModel();
    }
}