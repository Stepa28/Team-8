using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next
        , CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (RpcException ex)
        {
            string requestName = typeof(TRequest).Name;
            var uu = request.ToString();

            _logger.LogError(ex, "Status code: {@StatusCode} Message: {@Detail} Request: Unhandled Exception for Request {Name} {@Request}", ex.Status.StatusCode, ex.Status.Detail, requestName, uu);

            throw;
        }
        catch (Exception ex)
        {
            string requestName = typeof(TRequest).Name;
            var uu = request.ToString();

            _logger.LogError(ex, "Request: Unhandled Exception for Request {@Request}", requestName, uu);

            throw;
        }
    }
}