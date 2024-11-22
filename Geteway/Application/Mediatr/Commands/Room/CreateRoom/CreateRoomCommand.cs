using MediatR;

namespace Application.Mediatr.Commands.Room.CreateRoom;

public sealed record CreateRoomCommand(string Title, string Password) : IRequest<CreateRoomCommandModel>;