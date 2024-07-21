using Application.Mediator.Commands.ConnectRoom;
using Application.Mediator.Commands.CreateBattle;
using Application.Mediator.Commands.CreateRoom;
using Application.Mediator.Commands.DisconnectRoom;
using Application.Mediator.Commands.ToggleReadiness;
using Application.Mediator.Queries.GetRooms;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Team8.Contracts.Room.Server;

namespace Api.Services;

public class RoomServices(ISender mediator) : RoomService.RoomServiceBase
{
    public override async Task<RoomsModel> GetRooms(Empty request, ServerCallContext context)
    {
        var response = await mediator.Send(new GetRoomsQuery(), context.CancellationToken);
        return response;
    }

    public override async Task<Empty> ToggleReadiness(RoomId request, ServerCallContext context)
    {
        await mediator.Send(new ToggleReadinessCommand(request), context.CancellationToken);
        return new Empty();
    }

    public override async Task<Empty> StartBattle(RoomId request, ServerCallContext context)
    {
        await mediator.Send(new CreateBattleCommand(request), context.CancellationToken);
        return new Empty();
    }

    public override async Task<RoomId> CreateRoom(CreateRoomModel request, ServerCallContext context)
    {
        var response = await mediator.Send(new CreateRoomCommand(request), context.CancellationToken);
        return response;
    }

    public override async Task<RoomModel> ConnectRoom(ConnectRoomModel request, ServerCallContext context)
    {
        var response = await mediator.Send(new ConnectRoomCommand(request), context.CancellationToken);
        return response;
    }

    public override async Task<Empty> DisconnectRoom(RoomId request, ServerCallContext context)
    {
        await mediator.Send(new DisconnectRoomCommand(request), context.CancellationToken);
        return new Empty();
    }
}