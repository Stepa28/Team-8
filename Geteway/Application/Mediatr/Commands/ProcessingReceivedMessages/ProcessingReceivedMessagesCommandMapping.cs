using Application.Mediatr.Commands.ChoseMap;
using Application.Mediatr.Commands.ConnectRoom;
using Application.Mediatr.Commands.CreateRoom;
using Application.Mediatr.Commands.DisconnectRoom;
using Application.Mediatr.Commands.StartBattle;
using Application.Mediatr.Commands.ToggleReadiness;
using Application.Mediatr.Queries.GetMaps;
using Application.Mediatr.Queries.GetRooms;
using Riok.Mapperly.Abstractions;
using Team_8.Contracts.UserTransfer;

namespace Application.Mediatr.Commands.ProcessingReceivedMessages;

[Mapper]
public static partial class ProcessingReceivedMessagesCommandRequestMapping
{
    public static partial CreateRoomCommand MapToCreateRoomCommand(this CreateRoomDto query);
    public static partial ConnectRoomCommand MapToConnectRoomCommand(this ConnectRoomDto query);
    public static partial DisconnectRoomCommand MapToDisconnectRoomCommand(this IdDto query);
    public static partial ToggleReadinessCommand MapToToggleReadinessCommand(this IdDto query);
    public static partial StartBattleCommand MapToStartBattleCommand(this IdDto query);
    public static partial ChoseMapCommand MapToChoseMapCommand(this ChoseMapDto query);
    public static partial GetMapsQuery MapToGetMapsQuery(this EmptyDto query);
    public static partial GetRoomsQuery MapToStartBattleCommand(this EmptyDto query);
}

[Mapper]
public static partial class ProcessingReceivedMessagesCommandResponseMapping
{
    public static partial IdDto MapToIdDto(this CreateRoomCommandModel query);
    public static partial RoomDto MapToRoomDto(this ConnectRoomCommandModel query);
    public static partial RoomsDto MapToRoomsDto(this GetRoomsQueryModel query);
    public static partial RoomDto MapToRoomDto(this GetRoomQueryModel query);
    public static partial MapsInfoDto MapToMapsInfoDto(this GetMapsQueryModel query);
    public static partial MapInfoDto MapToMapInfoDto(this GetMapQueryModel query);
}