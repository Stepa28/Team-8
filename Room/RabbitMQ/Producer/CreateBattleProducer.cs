using Domain.Interfaces.Producers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Producer;

public class CreateBattleProducer(IBus bus, ILogger<CreateBattleProducer> logger) : ICreateBattleProducer
{
    public async Task CreateBattle(CreateBattleDto battleDto, CancellationToken stoppingToken = default)
    {
        logger.LogInformation("Отправка сообщения {@message}", battleDto);
        await bus.Publish(battleDto, stoppingToken);
    }
}