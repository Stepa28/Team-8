using Domain.Interfaces.Producers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Producer;

public class BattleStatusProducer(IBus bus, ILogger<BattleStatusProducer> logger) : IBattleStatusProducer
{
    public async Task PushUpdateBattleStatus(BattleStatusDto battleStatus, CancellationToken stoppingToken = default)
    {
        logger.LogInformation("Отправка сообщения {@message}", battleStatus);
        await bus.Publish(battleStatus, stoppingToken);
    }
}