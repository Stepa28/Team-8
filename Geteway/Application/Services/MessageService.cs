using Application.Mediatr.Commands.Battle.SendBattleState;
using Application.Mediatr.Commands.Room.SendUpdateRoom;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace Application.Services;

public class MessageService(ISender mediator, ILogger<MessageService> logger) : IMessageService {
    public async Task ProcessAddOrUpdateRoomMessage(AddOrUpdateRoomDto dto, CancellationToken cancellationToken = default) =>
        await mediator.Send(new SendUpdateRoomCommand(dto), cancellationToken);
    
    public async Task ProcessBattleStateMessage(BattleStateDto dto, CancellationToken cancellationToken = default) =>
        await mediator.Send(new SendBattleStateCommand(dto), cancellationToken);
}