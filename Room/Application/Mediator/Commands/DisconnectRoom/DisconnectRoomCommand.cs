using Application.Common.Attributes;
using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Producers;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.Enums;
using Team_8.Contracts.MassTransitDto;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Commands.DisconnectRoom;

[Authorize(Role.User)]
public sealed record DisconnectRoomCommand(RoomId Model) : IRequest;

internal sealed class DisconnectRoomCommandHandler(
    ILogger<DisconnectRoomCommandHandler> logger,
    IUserContext userContext,
    IUserJoinLeaveProducer producer,
    IRepository<UserState> repository,
    IMapper mapper)
    : IRequestHandler<DisconnectRoomCommand>
{
    public async Task Handle(DisconnectRoomCommand request, CancellationToken cancellationToken)
    {
        var state = await repository.GetAsync(x =>
                !x.IsDeleted
                && x.RoomId == request.Model.Id
                && x.UserId.Equals(userContext.User.Id)
            , cancellationToken
        );
        if(state == null)
            throw new NotFoundException("Вы неможите выйте из комноты, в которой не находитесь");

        var entity = mapper.Map<UserJoinLeaveDto>(userContext.User);
        await producer.PushUserJoinLeave(entity with { RoomId = request.Model.Id, State = UserJoinLeave.Leave }, cancellationToken);

        state.IsDeleted = true;
        await repository.SaveChangedAsync(cancellationToken);
    }
}