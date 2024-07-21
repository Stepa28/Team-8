using System.Text.Json;
using Domain.Common.Exceptions;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Api.Interceptors;

public class ApiExceptionInterceptor(ILogger<ApiExceptionInterceptor> logger) : Interceptor
{
    private readonly Dictionary<Type, Func<Exception, Exception>> _exceptionHandlers = new()
    {
        { typeof(ValidationException), HandleInvalidArgumentException }
        , { typeof(NotFoundException), HandleNotFoundException }
        , { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException }
        , { typeof(ForbiddenAccessException), HandlePermissionDeniedException }
        , { typeof(BadRequestException), HandleInternalException }
        , { typeof(JsonException), HandleInvalidArgumentException }
    };

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        logger.LogInformation("Starting receiving call. Type/Method: {Type} / {Method}", MethodType.Unary, context.Method);
        try
        {
            return await continuation(request, context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error thrown by {context.Method}.");
            throw HandleException(ex);
        }
    }

    private Exception HandleException(Exception ex)
    {
        var type = ex.GetType();
        return _exceptionHandlers.TryGetValue(type, out var handler) ? handler.Invoke(ex) : ex;
    }

    private static Exception HandleInvalidArgumentException(Exception ex)
    {
        return new RpcException(new Status(StatusCode.InvalidArgument, ex.Message, ex), ex.ToString());
    }

    private static Exception HandleNotFoundException(Exception ex)
    {
        return new RpcException(new Status(StatusCode.NotFound, ex.Message, ex), ex.ToString());
    }

    private static Exception HandleUnauthorizedAccessException(Exception ex)
    {
        return new RpcException(new Status(StatusCode.Unauthenticated, ex.Message, ex), ex.ToString());
    }

    private static Exception HandlePermissionDeniedException(Exception ex)
    {
        return new RpcException(new Status(StatusCode.PermissionDenied, ex.Message, ex), ex.ToString());
    }

    private static Exception HandleInternalException(Exception ex)
    {
        return new RpcException(new Status(StatusCode.Internal, ex.Message, ex), ex.ToString());
    }
}