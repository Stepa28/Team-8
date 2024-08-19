using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Commands.ConsumerWebSocket;

public sealed record ConsumerWebSocketCommand(WebSocketProvider Socket) : IRequest;

internal sealed class ConsumerWebSocketCommandHandler(
    ILogger<ConsumerWebSocketCommandHandler> logger
    , IMessageQuery query)
    : IRequestHandler<ConsumerWebSocketCommand>
{
    public async Task Handle(ConsumerWebSocketCommand request, CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var message = await request.Socket.ReceiveMessageAsync(cancellationToken);
                logger.LogInformation(message);
                query.AddMassage(request.Socket, message);
            }
        }
        catch(SocketCloseConnectException e)
        {
            await request.Socket.CloseConnection(e.Message, e.CloseStatus, cancellationToken);
        }
        catch(OperationCanceledException) {}
    }
}