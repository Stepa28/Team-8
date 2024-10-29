using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Protos.CodeFirst;

namespace Application.Mediator.Commands.MakeStep;

internal sealed class MakeStepCommandHandler(ILogger<MakeStepCommandHandler> logger) : IRequestHandler<MakeStepCommand, CurrentMapStatusModel>
{
    public async Task<CurrentMapStatusModel> Handle(MakeStepCommand request, CancellationToken cancellationToken)
    {
        return new CurrentMapStatusModel();
        //return request.MapToMakeStepCommandModel();
    }
}