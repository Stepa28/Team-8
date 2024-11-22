using Domain.Interfaces.Producers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.DTOs;

namespace RabbitMQ.Producer;

public class RemoveRoomProducer(IBus bus, ILogger<RemoveRoomProducer> logger) : IRemoveRoomProducer
{
    public async Task PushRemoveRoom(RoomInfoDto roomDto, CancellationToken stoppingToken = default)
    {
        logger.LogInformation("Отправка сообщения {@message}", roomDto);
        await bus.Publish(roomDto, stoppingToken);
    }
}