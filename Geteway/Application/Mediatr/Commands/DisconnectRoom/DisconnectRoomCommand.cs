using MediatR;

namespace Application.Mediatr.Commands.DisconnectRoom;

public sealed record DisconnectRoomCommand(int Id) : IRequest;