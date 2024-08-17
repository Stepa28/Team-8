using Team8.Contracts.Room.Service;

namespace Domain.Interfaces;

public interface IRoomService
{
    Task<RoomId> CreateRoom(CreateRoomModel model, CancellationToken cancellationToken = default);
    Task<RoomsModel> GetRooms(CancellationToken cancellationToken = default);
    Task<RoomModel> ConnectRoom(ConnectRoomModel model, CancellationToken cancellationToken = default);
    Task DisconnectRoom(RoomId model, CancellationToken cancellationToken = default);
    Task ToggleReadiness(RoomId model, CancellationToken cancellationToken = default);
    Task StartBattle(RoomId model, CancellationToken cancellationToken = default);
    Task<MapsShortModel> GetMaps(CancellationToken cancellationToken = default);
    Task ChoseMap(ChoseMapModel model, CancellationToken cancellationToken = default);
}