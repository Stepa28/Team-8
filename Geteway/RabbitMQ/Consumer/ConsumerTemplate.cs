using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.DTOs;

namespace RabbitMQ.Consumer;

public class ConsumerTemplate(ILogger<ConsumerTemplate> logger) : IConsumer<UserDto>
{
    public Task Consume(ConsumeContext<UserDto> context)
    {
        var jsonMessage = JsonSerializer.Serialize(context.Message);
        logger.LogInformation(jsonMessage);
        return Task.CompletedTask;
    }
}