using System.Text.Json;
using Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Consumer;

public class UserAddConsumer(ILogger<UserAddConsumer> logger, IMessageService service) : IConsumer<AddUserDto>
{
    public async Task Consume(ConsumeContext<AddUserDto> context)
    {
        var message = context.Message;
        var jsonMessage = JsonSerializer.Serialize(message);
        logger.LogInformation(jsonMessage);
        await service.ProcessUserAddMessage(message, context.CancellationToken);
    }
}