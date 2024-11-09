using System.Text.Json;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Commands.Battle.SendBattleState;

internal sealed class SendBattleStateCommandHandler(IWebSocketConnections connections, ILogger<SendBattleStateCommandHandler> logger) : IRequestHandler<SendBattleStateCommand>
{
    public async Task Handle(SendBattleStateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка нового состояния {@battleId}", request.Dto.BattleId);
        foreach (var unit in request.Dto.UnitList)
        {
            var connection = connections.ConnectionsFromUser(unit.UserId);
            if (connection is not null) 
                await connection.SendMessageAsync(JsonSerializer.Serialize(request.Dto.MapToBattleStateUpdateDto()), cancellationToken);
            else
            {
                logger.LogInformation("Пользователь с id {@UserId} не подключён", unit.UserId);
            }
        }
    }
}