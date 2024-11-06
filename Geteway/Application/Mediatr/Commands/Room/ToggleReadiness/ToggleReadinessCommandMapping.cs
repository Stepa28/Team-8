using Riok.Mapperly.Abstractions;
using Team8.Contracts.Room.Service;

namespace Application.Mediatr.Commands.Room.ToggleReadiness;

[Mapper]
public static partial class ToggleReadinessCommandMapping
{
    public static partial RoomId MapToRoomId(this ToggleReadinessCommand query);
}