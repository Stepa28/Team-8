using Domain.Interfaces.Producers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Producer;

public class UserDeleteProducer(IBus bus, ILogger<UserDeleteProducer> logger) : IUserDeleteProducer
{
    public async Task PushDeleteUser(DeleteUserDto userDto, CancellationToken stoppingToken = default)
    {
        logger.LogInformation("Отправка сообщения {@message}", userDto);
        await bus.Publish(userDto, stoppingToken);
    }
}