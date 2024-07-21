using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.DTOs;

namespace Application.Services;

public class MessageService(ILogger<MessageService> logger, ISender mediator) : IMessageService
{
    
    public void ProcessBattleStatusMessage(BattleStatusDto dto, CancellationToken cancellationToken = default)
    {
        
    }
    public void ProcessUserAddMessage(UserShortDto dto, CancellationToken cancellationToken = default)
    {
        
    }
    public void ProcessUserDeleteMessage(UserShortDto dto, CancellationToken cancellationToken = default)
    {
        
    }
}