using System.Text.Json;
using Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Consumer;

public class CreateBattleConsumer(ILogger<CreateBattleConsumer> logger, IMessageService service) : IConsumer<CreateBattleDto>
{
    public async Task Consume(ConsumeContext<CreateBattleDto> context)
    {
        var message = context.Message;
        var jsonMessage = JsonSerializer.Serialize(message);
        logger.LogInformation(jsonMessage);
        await service.ProcessCreateBattle(message, context.CancellationToken);
    }
}

