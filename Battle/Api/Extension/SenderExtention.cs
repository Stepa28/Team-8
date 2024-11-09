using MediatR;

namespace Api.Extension;

public static class SenderExtension
{
    public static Task<TResponse> CommandAsync<TResponse>(this ISender mediator, IRequest<TResponse> request
        , CancellationToken cancellationToken = default) =>
        mediator.Send(request, cancellationToken);

    public static Task CommandAsync<TRequest>(this ISender mediator, TRequest request, CancellationToken cancellationToken = default) =>
        mediator.Send(request, cancellationToken);
}