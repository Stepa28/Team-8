using MediatR;

namespace Application.Mediatr.Commands.ToggleReadiness;

public sealed record ToggleReadinessCommand(int Id) : IRequest;