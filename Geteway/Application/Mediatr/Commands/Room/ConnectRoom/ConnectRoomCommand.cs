using MediatR;

namespace Application.Mediatr.Commands.Room.ConnectRoom;

public sealed record ConnectRoomCommand(int Id, string Password) : IRequest<ConnectRoomCommandModel>;