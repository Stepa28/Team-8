using Riok.Mapperly.Abstractions;
using Team8.Contracts.Room.Service;

namespace Application.Mediatr.Commands.StartBattle;

[Mapper]
public static partial class StartBattleCommandMapping
{
    public static partial RoomId MapToRoomId(this StartBattleCommand query);
}