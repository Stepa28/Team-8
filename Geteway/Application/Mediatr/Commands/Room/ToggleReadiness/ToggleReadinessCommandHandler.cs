using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Commands.Room.ToggleReadiness;

internal sealed class ToggleReadinessCommandHandler(ILogger<ToggleReadinessCommandHandler> logger, IRoomService service) : IRequestHandler<ToggleReadinessCommand>
{
    public async Task Handle(ToggleReadinessCommand request, CancellationToken cancellationToken)
    {
        await service.ToggleReadiness(request.MapToRoomId(), cancellationToken);
    }
}