using Domain.Common;
using Domain.Common.Exceptions;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException }
            , { typeof(NotFoundException), HandleNotFoundException }
            , { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException }
            , { typeof(ForbiddenAccessException), HandleForbiddenAccessException }
            , { typeof(BadRequestException), HandleBadRequestException }
            , { typeof(RpcException), HandleGrpcException }
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if(_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if(!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }
    }

    private void HandleValidationException(ExceptionContext context)
    {
        ValidationException exception = (ValidationException)context.Exception;

        ValidationProblemDetails details = new(exception.Errors) { Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1" };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        ValidationProblemDetails details = new(context.ModelState) { Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1" };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        NotFoundException exception = (NotFoundException)context.Exception;

        ProblemDetails details = new()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            , Title = "The specified resource was not found."
            , Detail = exception.Message
        };

        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }

    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        ProblemDetails details = new()
        {
            Status = StatusCodes.Status401Unauthorized, Title = "Unauthorized", Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };

        context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status401Unauthorized };

        context.ExceptionHandled = true;
    }

    private void HandleForbiddenAccessException(ExceptionContext context)
    {
        ProblemDetails details = new()
        {
            Status = StatusCodes.Status403Forbidden
            , Title = "У Вас нет прав для выполнения этой операции"
            , Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            , Detail = context.Exception.Message
        };

        context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status403Forbidden };

        context.ExceptionHandled = true;
    }

    private void HandleBadRequestException(ExceptionContext context)
    {
        ProblemDetails details = new()
        {
            Status = StatusCodes.Status400BadRequest
            , Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            , Detail = context.Exception.Message
        };

        context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status400BadRequest };

        context.ExceptionHandled = true;
    }

    private void HandleGrpcException(ExceptionContext context)
    {
        RpcException ex = (RpcException)context.Exception;

        ProblemDetails details = new()
        {
            Status = StatusCodes.Status502BadGateway, Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1", Detail = ex.Message
        };

        if(context.HttpContext.WebSockets.IsWebSocketRequest)
        {
            if(Instances.Connections.Connection(context.HttpContext) is {} socket)
                socket.SendMessageAsync(details, CancellationToken.None).Wait();
        }
        else
        {
            context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status502BadGateway };
            


            context.ExceptionHandled = true;
        }
    }
}