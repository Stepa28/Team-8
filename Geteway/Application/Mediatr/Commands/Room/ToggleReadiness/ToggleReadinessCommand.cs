using MediatR;

namespace Application.Mediatr.Commands.Room.ToggleReadiness;

public sealed record ToggleReadinessCommand(int Id) : IRequest;