using Grpc.Core;
using Team8.Contracts.Room.Server;
using Void = Team8.Contracts.Room.Server.Void;

namespace Api.Services;

public class RoomServices : RoomService.RoomServiceBase
{
    public override Task<RoomId> CreateRoom(CreateRoomModel request, ServerCallContext context)
    {
        return base.CreateRoom(request, context);
    }

    public override Task<RoomsModel> GetRooms(Void request, ServerCallContext context)
    {
        return base.GetRooms(request, context);
    }

    public override Task<RoomModel> ConnectRoom(ConnectRoomModel request, ServerCallContext context)
    {
        return base.ConnectRoom(request, context);
    }

    public override Task<RequestModel> DisconnectRoom(RoomActionModel request, ServerCallContext context)
    {
        return base.DisconnectRoom(request, context);
    }

    public override Task<RequestModel> ToggleReadiness(RoomActionModel request, ServerCallContext context)
    {
        return base.ToggleReadiness(request, context);
    }

    public override Task<RequestModel> StartBattle(RoomActionModel request, ServerCallContext context)
    {
        return base.StartBattle(request, context);
    }
}