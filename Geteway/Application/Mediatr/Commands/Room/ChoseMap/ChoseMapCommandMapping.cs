using Riok.Mapperly.Abstractions;
using Team8.Contracts.Room.Service;

namespace Application.Mediatr.Commands.Room.ChoseMap;

[Mapper]
public static partial class ChoseMapCommandMapping
{
    [MapProperty(nameof(ChoseMapCommand.MapId), nameof(ChoseMapModel.Map), Use = nameof(MapMap))]
    [MapProperty(nameof(ChoseMapCommand.RoomId), nameof(ChoseMapModel.Room), Use = nameof(MapRoom))]
    public static partial ChoseMapModel MapToChoseMapModel(this ChoseMapCommand query);
    
    [UserMapping(Default = false)]
    private static MapId MapMap(int mapId) => new() {Id = mapId};
    
    [UserMapping(Default = false)]
    private static RoomId MapRoom(int roomId) => new() {Id = roomId};
}