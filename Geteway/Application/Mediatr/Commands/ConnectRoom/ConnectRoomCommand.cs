using MediatR;

namespace Application.Mediatr.Commands.ConnectRoom;

public sealed record ConnectRoomCommand(int Id, string Password) : IRequest<ConnectRoomCommandModel>;