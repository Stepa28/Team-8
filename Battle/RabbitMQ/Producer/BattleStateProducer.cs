using Domain.Interfaces.Producers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Producer;

public class BattleStateProducer(IBus bus, ILogger<BattleStateProducer> logger) : IBattleStateProducer
{
    public async Task PushUpdateBattleState(BattleStateDto battleStatus, CancellationToken stoppingToken = default)
    {
        logger.LogInformation("Отправка сообщения {@message}", battleStatus);
        await bus.Publish(battleStatus, stoppingToken);
    }
}