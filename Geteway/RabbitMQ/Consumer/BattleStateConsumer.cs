using System.Text.Json;
using Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Consumer;

public class BattleStateConsumer(ILogger<BattleStateConsumer> logger, IMessageService service) : IConsumer<BattleStateDto>
{
    public async Task Consume(ConsumeContext<BattleStateDto> context)
    {
        var jsonMessage = JsonSerializer.Serialize(context.Message);
        logger.LogInformation($"Получено сообщение из RabbitMQ {jsonMessage}");
        await service.ProcessBattleStateMessage(context.Message);
    }
}