using Application.Common.WebSocket;
using Domain.Common;
using Domain.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Commands.ConsumerWebSocket;

public sealed record ConsumerWebSocketCommand(WebSocketProvider Socket) : IRequest;

internal sealed class ConsumerWebSocketCommandHandler(ILogger<ConsumerWebSocketCommandHandler> logger, IMessageQuery query)
    : IRequestHandler<ConsumerWebSocketCommand>
{
    public async Task Handle(ConsumerWebSocketCommand request, CancellationToken cancellationToken)
    {
        try
        {
            while (true)
            {
                var message = await request.Socket.ReceiveMessageAsync(cancellationToken);
                logger.LogInformation(message);
                await request.Socket.SendMessageAsync(message, cancellationToken);
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