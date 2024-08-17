using Domain.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Team8.Contracts.Room.Service;

namespace Infrastructure.Services;

public class RoomService(
    Team8.Contracts.Room.Service.RoomService.RoomServiceClient client
    , ILogger<AuthService> logger
    , IContext context
    , IConfiguration config) : BaseService(context, config), IRoomService
{
    public async Task<RoomId> CreateRoom(CreateRoomModel model, CancellationToken cancellationToken = default) =>
        await client.CreateRoomAsync(model, GetCallOptions(cancellationToken));
    public async Task<RoomsModel> GetRooms(CancellationToken cancellationToken = default) =>
        await client.GetRoomsAsync(new Empty(), GetCallOptions(cancellationToken));
    public async Task<RoomModel> ConnectRoom(ConnectRoomModel model, CancellationToken cancellationToken = default) =>
        await client.ConnectRoomAsync(model, GetCallOptions(cancellationToken));
    public async Task DisconnectRoom(RoomId model, CancellationToken cancellationToken = default) =>
        await client.DisconnectRoomAsync(model, GetCallOptions(cancellationToken));
    public async Task ToggleReadiness(RoomId model, CancellationToken cancellationToken = default) =>
        await client.ToggleReadinessAsync(model, GetCallOptions(cancellationToken));
    public async Task StartBattle(RoomId model, CancellationToken cancellationToken = default) =>
        await client.StartBattleAsync(model, GetCallOptions(cancellationToken));
    public async Task<MapsShortModel> GetMaps(CancellationToken cancellationToken = default) =>
        await client.GetMapsAsync(new Empty(), GetCallOptions(cancellationToken));
    public async Task ChoseMap(ChoseMapModel model, CancellationToken cancellationToken = default) =>
        await client.ChoseMapAsync(model, GetCallOptions(cancellationToken));
}