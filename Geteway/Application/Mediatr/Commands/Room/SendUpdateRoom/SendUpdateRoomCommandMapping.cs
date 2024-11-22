using Riok.Mapperly.Abstractions;
using Team_8.Contracts.MassTransitDto;
using Team_8.Contracts.UserTransfer;

namespace Application.Mediatr.Commands.Room.SendUpdateRoom;

[Mapper]
public static partial class SendUpdateRoomCommandMapping
{
    public static partial UpdateRoomDto MapToUpdateRoomDto(this AddOrUpdateRoomDto query);
    public static partial RoomDto MapToRoomDto(this AddOrUpdateRoomDto query);
}