using MediatR;

namespace Application.Mediatr.Commands.ChoseMap;

public sealed record ChoseMapCommand(int RoomId, int MapId) : IRequest;