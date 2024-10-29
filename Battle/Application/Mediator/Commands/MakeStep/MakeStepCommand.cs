using MediatR;
using Team_8.Contracts.Protos.CodeFirst;

namespace Application.Mediator.Commands.MakeStep;

public sealed record MakeStepCommand(StepModel Model) : IRequest<CurrentMapStatusModel>;