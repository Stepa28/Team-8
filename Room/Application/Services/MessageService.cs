using Application.Mediator.Commands.AddUser;
using Application.Mediator.Commands.BattleStatusUpdate;
using Application.Mediator.Commands.DeleteUser;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace Application.Services;

public class MessageService(ILogger<MessageService> logger, ISender mediator) : IMessageService
{
    public async Task ProcessBattleStatusMessage(BattleStatusDto dto, CancellationToken cancellationToken = default) =>
        await mediator.Send(new BattleStatusUpdateCommand(dto), cancellationToken);

    public async Task ProcessUserAddMessage(AddUserDto dto, CancellationToken cancellationToken = default) =>
        await mediator.Send(new AddUserCommand(dto), cancellationToken);

    public async Task ProcessUserDeleteMessage(DeleteUserDto dto, CancellationToken cancellationToken = default) =>
        await mediator.Send(new DeleteUserCommand(dto), cancellationToken);
}