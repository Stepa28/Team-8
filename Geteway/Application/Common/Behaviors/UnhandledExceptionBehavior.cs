using System.Net.WebSockets;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger, IContext context) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull where TResponse : new()
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next
        , CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch(RpcException ex)
        {
            var requestName = typeof(TRequest).Name;

            logger.LogError(ex, "Status code: {@StatusCode} Message: {@Detail} Request: Unhandled Exception for Request {Name} {@Request}"
                , ex.Status.StatusCode, ex.Status.Detail, requestName, request.ToString());

            if(context.SocketProvider is { WebSocket.State: WebSocketState.Open })
                await context.SocketProvider.SendMessageAsync(
                    $"Status code: {ex.Status.StatusCode} Message: {ex.Status.Detail} Request: Unhandled Exception for Request {requestName} {request.ToString()}"
                    , cancellationToken);

            throw;
        }
        catch(ValidationException ex)
        {
            var requestName = typeof(TRequest).Name;

            logger.LogError(ex, "ValidationException: Errors {@Errors} {@Ditails} for Request {Name} {@Request}", ex.Message,
                ex.Errors.Values.Select(x => string.Join(' ', x)), requestName, request.ToString());

            if(context.SocketProvider is { WebSocket.State: WebSocketState.Open })
                await context.SocketProvider.SendMessageAsync(
                    $"ValidationException {new string(ex.Errors.Values.SelectMany(x => string.Join(' ', x)).ToArray() )}", cancellationToken);

            throw;
        }
        catch(Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            logger.LogError(ex, "Request: Unhandled Exception for Request {Name} {@Request}", requestName, request.ToString());

            if(context.SocketProvider is { WebSocket.State: WebSocketState.Open })
                await context.SocketProvider.SendMessageAsync(
                    $"Exception {ex}", cancellationToken);

            throw;
        }
    }
}