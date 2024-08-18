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
            string requestName = typeof(TRequest).Name;
            var uu = request.ToString();


            logger.LogError(ex, "Status code: {@StatusCode} Message: {@Detail} Request: Unhandled Exception for Request {Name} {@Request}"
                , ex.Status.StatusCode, ex.Status.Detail, requestName, uu);

            if(context.SocketProvider != null)
                await context.SocketProvider.SendMessageAsync(
                    $"Status code: {ex.Status.StatusCode} Message: {ex.Status.Detail} Request: Unhandled Exception for Request {requestName} {uu}"
                    , cancellationToken);

            throw;
        }
        catch(Exception ex)
        {
            string requestName = typeof(TRequest).Name;
            var uu = request.ToString();

            logger.LogError(ex, "Request: Unhandled Exception for Request {@Request} {@ff}", requestName, uu);
            
            if(context.SocketProvider != null)
                await context.SocketProvider.SendMessageAsync(
                    $"Exception {ex}", cancellationToken);

            throw;
        }
    }
}