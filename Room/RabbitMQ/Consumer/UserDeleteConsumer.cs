using System.Text.Json;
using Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Consumer;

public class UserDeleteConsumer(ILogger<UserDeleteConsumer> logger, IMessageService service) : IConsumer<DeleteUserDto>
{
    public async Task Consume(ConsumeContext<DeleteUserDto> context)
    {
        var message = context.Message;
        var jsonMessage = JsonSerializer.Serialize(message);
        logger.LogInformation(jsonMessage);
        await service.ProcessUserDeleteMessage(message, context.CancellationToken);
    }
}