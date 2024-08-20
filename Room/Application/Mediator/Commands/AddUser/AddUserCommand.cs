using Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace Application.Mediator.Commands.AddUser;

public sealed record AddUserCommand(AddUserDto Model) : IRequest;

internal sealed class AddUserCommandHandler(ILogger<AddUserCommandHandler> logger, IUserRepository repository) : IRequestHandler<AddUserCommand>
{
    public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        await repository.CreateAsync(request.Model.Id, cancellationToken);
    }
}