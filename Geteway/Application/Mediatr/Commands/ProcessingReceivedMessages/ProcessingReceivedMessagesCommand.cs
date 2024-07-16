using System.Collections.Concurrent;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Commands.ProcessingReceivedMessages;

public sealed record ProcessingReceivedMessagesCommand(WebSocketProvider Socket, string Massage) : IRequest;

internal sealed class ProcessingReceivedMessagesCommandHandler(ILogger<ProcessingReceivedMessagesCommandHandler> logger, ISender sender) : IRequestHandler<ProcessingReceivedMessagesCommand>
{
    public async Task Handle(ProcessingReceivedMessagesCommand request, CancellationToken cancellationToken)
    {
        
    }
}