using Domain.Common.Exceptions;
using Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace Application.Mediator.Commands.DeleteUser;

public sealed record DeleteUserCommand(DeleteUserDto Model) : IRequest;

internal sealed class DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IUserRepository repository)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if(!await repository.DeleteAsync(request.Model.Id, cancellationToken))
            throw new NotFoundException($"Пользователь с Id = {request.Model.Id} не найден");
    }
}