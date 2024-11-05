using Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace Application.Mediator.Commands.AddUser;

public sealed record AddUserCommand(AddUserDto Model) : IRequest;

internal sealed class AddUserCommandHandler(ILogger<AddUserCommandHandler> logger, IUserRepository repository) : IRequestHandler<AddUserCommand>
{
    /// <summary>
    /// Обрабатывает команду <see cref="AddUserCommand"/>, добавляя нового пользователя в репозиторий.
    /// </summary>
    /// <param name="request">Инстанс <see cref="AddUserCommand"/>, содержащий данные нового пользователя.</param>
    /// <param name="cancellationToken">Токен отмены, который может использоваться другими объектами или потоками для получения уведомления об отмене.</param>
    /// <returns>Задача, представляющая асинхронную операцию. Результат задачи не содержит значения.</returns>
    public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        await repository.CreateAsync(request.Model.Id, cancellationToken);
    }
}