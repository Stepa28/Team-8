using Riok.Mapperly.Abstractions;
using Team8.Contracts.Room.Service;

namespace Application.Mediatr.Commands.Room.CreateRoom;

[Mapper]
public static partial class CreateRoomCommandMapping
{
    public static partial CreateRoomCommandModel MapToCreateRoomCommandModel(this RoomId query);
    [MapProperty(nameof(CreateRoomCommand.Password), nameof(CreateRoomModel.Pass))]
    public static partial CreateRoomModel MapToCreateRoomModel(this CreateRoomCommand query);
}