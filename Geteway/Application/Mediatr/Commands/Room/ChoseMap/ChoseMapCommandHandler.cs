using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Commands.Room.ChoseMap;

internal sealed class ChoseMapCommandHandler(ILogger<ChoseMapCommandHandler> logger, IRoomService service) : IRequestHandler<ChoseMapCommand>
{
    public async Task Handle(ChoseMapCommand request, CancellationToken cancellationToken)
    {
        await service.ChoseMap(request.MapToChoseMapModel(), cancellationToken);
    }
}