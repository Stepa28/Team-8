﻿using Application.Mediator.Commands;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.MassTransitDto;

namespace Application.Services;

public class MessageService(ILogger<MessageService> logger, ISender mediator) : IMessageService
{
    public async Task ProcessCreateBattle(CreateBattleDto dto, CancellationToken cancellationToken = default) =>
        await mediator.Send(new CreateBattleCommand(dto), cancellationToken);
}