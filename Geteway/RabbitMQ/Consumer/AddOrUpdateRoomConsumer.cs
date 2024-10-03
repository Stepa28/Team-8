using System.Text.Json;
using Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace RabbitMQ.Consumer;

public class AddOrUpdateRoomConsumer(ILogger<AddOrUpdateRoomConsumer> logger, IMessageService service) : IConsumer<AddOrUpdateRoomDto>
{
    public async Task Consume(ConsumeContext<AddOrUpdateRoomDto> context)
    {
        var jsonMessage = JsonSerializer.Serialize(context.Message);
        logger.LogInformation($"Получено сообщение из RabbitMQ {jsonMessage}");
        await service.ProcessAddOrUpdateRoomMessage(context.Message);
    }
}