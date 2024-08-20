﻿using Application.Mediatr.Commands.SendUpdateRoom;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace Application.Services;

public class MessageService(ILogger<MessageService> logger, ISender mediator) : IMessageService
{
    public async Task ProcessAddOrUpdateRoomMessage(AddOrUpdateRoomDto dto, CancellationToken cancellationToken = default) =>
        await mediator.Send(new SendUpdateRoomCommand(dto), cancellationToken);
}