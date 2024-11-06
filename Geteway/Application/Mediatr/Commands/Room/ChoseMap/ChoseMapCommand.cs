using MediatR;

namespace Application.Mediatr.Commands.Room.ChoseMap;

public sealed record ChoseMapCommand(int RoomId, int MapId) : IRequest;