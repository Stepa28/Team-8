using MediatR;
using Team_8.Contracts.MassTransitDto;

namespace Application.Mediatr.Commands.Room.SendUpdateRoom;

public sealed record SendUpdateRoomCommand(AddOrUpdateRoomDto Dto) : IRequest;