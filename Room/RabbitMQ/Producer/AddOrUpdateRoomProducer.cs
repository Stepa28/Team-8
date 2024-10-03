using Domain.Interfaces.Producers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Producer;


public class AddOrUpdateRoomProducer(IBus bus, ILogger<AddOrUpdateRoomProducer> logger) : IAddOrUpdateRoomProducer
{
    public async Task PushAddOrUpdateRoom(AddOrUpdateRoomDto roomDto, CancellationToken stoppingToken = default)
    {
        logger.LogInformation("Отправка сообщения {@message}", roomDto);
        await bus.Publish(roomDto, stoppingToken);
    }
}