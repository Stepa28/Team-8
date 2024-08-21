using System.Text.Json;
using Application.Mediatr.Commands.ConnectRoom;
using Application.Mediatr.Commands.CreateRoom;
using Application.Mediatr.Queries.GetMaps;
using Application.Mediatr.Queries.GetRooms;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Common;
using Team_8.Contracts.Enums;
using Team_8.Contracts.UserTransfer;

namespace Application.Mediatr.Commands.ProcessingReceivedMessages;

internal sealed class ProcessingReceivedMessagesCommandHandler(
    ILogger<ProcessingReceivedMessagesCommandHandler> logger,
    ISender sender,
    IContext context) : IRequestHandler<ProcessingReceivedMessagesCommand>
{
    static ProcessingReceivedMessagesCommandHandler()
    {
        InputMappers = new()
        {
            { PurposeOfTheMessage.CreateRoom, dto => (dto as CreateRoomDto)?.MapToCreateRoomCommand() ?? throw BR },
            { PurposeOfTheMessage.ConnectRoom, dto => (dto as ConnectRoomDto)?.MapToConnectRoomCommand() ?? throw BR },
            { PurposeOfTheMessage.DisconnectRoom, dto => (dto as IdDto)?.MapToDisconnectRoomCommand() ?? throw BR },
            { PurposeOfTheMessage.ToggleReadiness, dto => (dto as IdDto)?.MapToToggleReadinessCommand() ?? throw BR },
            { PurposeOfTheMessage.StartBattle, dto => (dto as IdDto)?.MapToStartBattleCommand() ?? throw BR },
            { PurposeOfTheMessage.ChoseMap, dto => (dto as ChoseMapDto)?.MapToChoseMapCommand() ?? throw BR },
            { PurposeOfTheMessage.GetMaps, dto => (dto as EmptyDto)?.MapToGetMapsQuery() ?? throw BR },
            { PurposeOfTheMessage.GetRooms, dto => (dto as EmptyDto)?.MapToStartBattleCommand() ?? throw BR }
        };
        OutputMappers = new()
        {
            { PurposeOfTheMessage.CreateRoom, responseModel => (responseModel as CreateRoomCommandModel)?.MapToIdDto() ?? throw BR },
            { PurposeOfTheMessage.ConnectRoom, responseModel => (responseModel as ConnectRoomCommandModel)?.MapToRoomDto() ?? throw BR },
            { PurposeOfTheMessage.DisconnectRoom, responseModel => responseModel is Unit ? new EmptyDto() : throw BR },
            { PurposeOfTheMessage.ToggleReadiness, responseModel => responseModel is Unit ? new EmptyDto() : throw BR },
            { PurposeOfTheMessage.StartBattle, responseModel => responseModel is Unit ? new EmptyDto() : throw BR },
            { PurposeOfTheMessage.ChoseMap, responseModel => responseModel is Unit ? new EmptyDto() : throw BR },
            { PurposeOfTheMessage.GetMaps, responseModel => (responseModel as GetMapsQueryModel)?.MapToMapsInfoDto() ?? throw BR },
            { PurposeOfTheMessage.GetRooms, responseModel => (responseModel as GetRoomsQueryModel)?.MapToRoomsDto() ?? throw BR }
        };
    }
    
    private static BadRequestException BR => new BadRequestException("Не коректный запрос");
    private static Dictionary<PurposeOfTheMessage, Func<object?, IBaseRequest>> InputMappers { get; }
    private static Dictionary<PurposeOfTheMessage, Func<object?, IUserTransferDto>> OutputMappers { get; }
    

    public async Task Handle(ProcessingReceivedMessagesCommand request, CancellationToken cancellationToken)
    {
        context.SocketProvider = request.Socket;

        var message = JsonSerializer.Deserialize<UserMassageTransfer>(request.Message);

        if(message == null || !message.TypeMessage.Equals(message.Purpose.GetRequestDtoName()))
            throw BR;

        var dto = JsonSerializer.Deserialize(message.Message, message.Purpose.GetRequestDtoType() ?? typeof(EmptyDto));

        if(dto is null || !InputMappers.TryGetValue(message.Purpose, out var funcIn))
            throw BR;

        var command = funcIn(dto);
        object? responseModel;
        try
        {
            responseModel = await sender.Send(command, cancellationToken);
        }
        catch(ValidationException)
        {
            throw;
        }
        catch
        {
            return;
        }
        
        if(responseModel is null || !OutputMappers.TryGetValue(message.Purpose, out var funcOut))
            throw BR;

        var response = message with
        {
            TypeMessage = message.Purpose.GetResponseDtoName(),
            Message = JsonSerializer.Serialize(funcOut(responseModel), message.Purpose.GetResponseDtoType() ?? typeof(EmptyDto))
        };
        await request.Socket.SendMessageAsync(response, cancellationToken);
    }
}