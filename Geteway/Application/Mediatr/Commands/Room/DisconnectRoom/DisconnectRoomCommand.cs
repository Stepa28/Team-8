using MediatR;

namespace Application.Mediatr.Commands.Room.DisconnectRoom;

public sealed record DisconnectRoomCommand(int Id) : IRequest;