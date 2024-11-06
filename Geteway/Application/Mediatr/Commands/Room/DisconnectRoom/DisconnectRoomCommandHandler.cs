using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Commands.Room.DisconnectRoom;

internal sealed class DisconnectRoomCommandHandler(ILogger<DisconnectRoomCommandHandler> logger, IRoomService service) : IRequestHandler<DisconnectRoomCommand>
{
    public async Task Handle(DisconnectRoomCommand request, CancellationToken cancellationToken)
    {
        await service.DisconnectRoom(request.MapToRoomId(), cancellationToken);
    }
}