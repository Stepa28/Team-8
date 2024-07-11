using Api.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private ISender Mediator => HttpContext.RequestServices.GetRequiredService<ISender>();

    protected Task<TResponse> QueryAsync<TResponse>(IRequest<TResponse> request) => Mediator.Send(request, HttpContext.RequestAborted);

    protected Task QueryAsync(IRequest request) => Mediator.Send(request, HttpContext.RequestAborted);

    protected Task<TResponse> CommandAsync<TResponse>(IRequest<TResponse> request
        , CancellationToken cancellationToken = default) =>
        Mediator.Send(request, cancellationToken);

    protected Task CommandAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) =>
        Mediator.Send(request, cancellationToken);
}