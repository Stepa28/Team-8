using Riok.Mapperly.Abstractions;
using Team8.Contracts.Room.Service;

namespace Application.Mediatr.Commands.Room.ConnectRoom;

[Mapper]
public static partial class ConnectRoomCommandMapping
{
    public static partial ConnectRoomCommandModel MapToConnectRoomCommandModel(this RoomModel query);
    [MapProperty(nameof(ConnectRoomCommand.Password), nameof(ConnectRoomModel.Pass))]
    public static partial ConnectRoomModel MapToConnectRoomModel(this ConnectRoomCommand query);
}