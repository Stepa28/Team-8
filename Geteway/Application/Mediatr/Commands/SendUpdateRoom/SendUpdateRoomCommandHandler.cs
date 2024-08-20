using System.Text.Json;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Common;
using Team_8.Contracts.Enums;
using Team_8.Contracts.UserTransfer;

namespace Application.Mediatr.Commands.SendUpdateRoom;

internal sealed class SendUpdateRoomCommandHandler(ILogger<SendUpdateRoomCommandHandler> logger, IWebSocketConnections connections)
    : IRequestHandler<SendUpdateRoomCommand>
{
    public async Task Handle(SendUpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var template = new UserMassageTransfer(Microservice.Room, PurposeOfTheMessage.UpdateRoom,
            PurposeOfTheMessage.UpdateRoom.GetResponseDtoName(), "");
        foreach (var connect in connections.ActiveConnections())
        {
            if(request.Dto.Type == RoomUpdateType.Add)
                await connect.SendMessageAsync(
                    template with { Purpose = PurposeOfTheMessage.NewRoom, Message = JsonSerializer.Serialize(request.Dto.MapToRoomDto()) },
                    cancellationToken);
            else
                await connect.SendMessageAsync(
                    template with { Message = JsonSerializer.Serialize(request.Dto.MapToUpdateRoomDto()) },
                    cancellationToken);
        }
    }
}