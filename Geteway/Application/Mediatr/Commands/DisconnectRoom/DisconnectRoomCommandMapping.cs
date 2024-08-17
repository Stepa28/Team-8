using Riok.Mapperly.Abstractions;
using Team8.Contracts.Room.Service;

namespace Application.Mediatr.Commands.DisconnectRoom;

[Mapper]
public static partial class DisconnectRoomCommandMapping
{
    public static partial RoomId MapToRoomId(this DisconnectRoomCommand query);
}