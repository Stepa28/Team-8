using System.Text.Json;
using Application.Mediatr.Commands.ConnectRoom;
using Application.Mediatr.Commands.CreateRoom;
using Application.Mediatr.Queries.GetMaps;
using Application.Mediatr.Queries.GetRooms;
using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Common;
using Team_8.Contracts.Enums;
using Team_8.Contracts.UserTransfer;

namespace Application.Mediatr.Commands.ProcessingReceivedMessages;

public sealed record ProcessingReceivedMessagesCommand(WebSocketProvider Socket, string Message) : IRequest;

internal sealed class ProcessingReceivedMessagesCommandHandler(
    ILogger<ProcessingReceivedMessagesCommandHandler> logger,
    ISender sender,
    IContext context) : IRequestHandler<ProcessingReceivedMessagesCommand>
{
    public async Task Handle(ProcessingReceivedMessagesCommand request, CancellationToken cancellationToken)
    {
        context.SocketProvider = request.Socket;

        var message = JsonSerializer.Deserialize<UserMassageTransfer>(request.Message);

        if(message == null || !message.TypeMessage.Equals(message.Purpose.GetRequestDtoName()))
            throw new BadRequestException("Не коректный запрос");

        var dto = JsonSerializer.Deserialize(message.Message, message.Purpose.GetRequestDtoType() ?? typeof(EmptyDto));

        if(dto is null)
            throw new BadRequestException("Не коректный запрос");

        IBaseRequest command = message.Purpose switch
        {
            PurposeOfTheMessage.CreateRoom when dto is CreateRoomDto x => x.MapToCreateRoomCommand(),
            PurposeOfTheMessage.ConnectRoom when dto is ConnectRoomDto x => x.MapToConnectRoomCommand(),
            PurposeOfTheMessage.DisconnectRoom when dto is IdDto x => x.MapToDisconnectRoomCommand(),
            PurposeOfTheMessage.ToggleReadiness when dto is IdDto x => x.MapToToggleReadinessCommand(),
            PurposeOfTheMessage.StartBattle when dto is IdDto x => x.MapToStartBattleCommand(),
            PurposeOfTheMessage.ChoseMap when dto is ChoseMapDto x => x.MapToChoseMapCommand(),
            PurposeOfTheMessage.GetMaps when dto is EmptyDto x => x.MapToGetMapsQuery(),
            PurposeOfTheMessage.GetRooms when dto is EmptyDto x => x.MapToStartBattleCommand(),
            _ => throw new BadRequestException("Не коректный запрос")
        };

        var responseModel = await sender.Send(command, cancellationToken);

        IUserTransferDto responseDto = message.Purpose switch
        {
            PurposeOfTheMessage.CreateRoom when responseModel is CreateRoomCommandModel x => x.MapToIdDto(),
            PurposeOfTheMessage.ConnectRoom when responseModel is ConnectRoomCommandModel x => x.MapToRoomDto(),
            PurposeOfTheMessage.DisconnectRoom when responseModel is null => new EmptyDto(),
            PurposeOfTheMessage.ToggleReadiness when responseModel is null => new EmptyDto(),
            PurposeOfTheMessage.StartBattle when responseModel is null => new EmptyDto(),
            PurposeOfTheMessage.ChoseMap when responseModel is null => new EmptyDto(),
            PurposeOfTheMessage.GetMaps when responseModel is GetRoomsQueryModel x => x.MapToRoomsDto(),
            PurposeOfTheMessage.GetRooms when responseModel is GetMapsQueryModel x => x.MapToMapsInfoDto(),
            _ => throw new BadRequestException("Не коректный запрос")
        };

        var response = message with
        {
            TypeMessage = message.Purpose.GetResponseDtoName(),
            Message = JsonSerializer.Serialize(responseDto, message.Purpose.GetResponseDtoType() ?? typeof(EmptyDto))
        };
        await request.Socket.SendMessageAsync(response, cancellationToken);
    }
}