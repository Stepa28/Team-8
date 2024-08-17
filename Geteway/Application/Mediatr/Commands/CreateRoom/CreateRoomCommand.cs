using MediatR;

namespace Application.Mediatr.Commands.CreateRoom;

public sealed record CreateRoomCommand(string Title, string Password) : IRequest<CreateRoomCommandModel>;