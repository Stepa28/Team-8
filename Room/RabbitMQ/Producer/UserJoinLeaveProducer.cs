using Domain.Interfaces.Producers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Producer;

public class UserJoinLeaveProducer(IBus bus, ILogger<UserJoinLeaveProducer> logger) : IUserJoinLeaveProducer
{
    public async Task PushUserJoinLeave(UserJoinLeaveDto userDto, CancellationToken stoppingToken = default)
    {
        logger.LogInformation("Отправка сообщения {@message}", userDto);
        await bus.Publish(userDto, stoppingToken);
    }
}