using Domain.Interfaces.Producers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Producer;


public class UserAddProducer(IBus bus, ILogger<UserAddProducer> logger) : IUserAddProducer
{
    public async Task PushAddUser(AddUserDto roomDto, CancellationToken stoppingToken = default)
    {
        logger.LogInformation("Отправка сообщения {@message}", roomDto);
        await bus.Publish(roomDto, stoppingToken);
    }
}