using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Commands.StartBattle;

internal sealed class StartBattleCommandHandler(ILogger<StartBattleCommandHandler> logger, IRoomService service) : IRequestHandler<StartBattleCommand>
{
    public async Task Handle(StartBattleCommand request, CancellationToken cancellationToken)
    {
        await service.StartBattle(request.MapToRoomId(), cancellationToken);
    }
}