using System.Text.Json;
using Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Consumer;

public class BattleStatusConsumer(ILogger<BattleStatusConsumer> logger, IMessageService service) : IConsumer<BattleStatusDto>
{
    public async Task Consume(ConsumeContext<BattleStatusDto> context)
    {
        var message = context.Message;
        var jsonMessage = JsonSerializer.Serialize(message);
        logger.LogInformation(jsonMessage);
        await service.ProcessBattleStatusMessage(message, context.CancellationToken);
    }
}