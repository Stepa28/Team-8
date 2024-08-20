using Application.Mediatr.Commands.ConsumerWebSocket;
using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Team8.Contracts.Auth.Service;

namespace Application.Mediatr.Commands.ConnectionWebSocket;

public sealed record ConnectionWebSocketCommand(HttpContext Context) : IRequest;

internal sealed class ConnectionWebSocketCommandHandler(
    ILogger<ConnectionWebSocketCommandHandler> logger
    , IWebSocketConnections connections
    , IAuthService auth
    , ISender sender
    , IConfiguration config) : IRequestHandler<ConnectionWebSocketCommand>
{
    public async Task Handle(ConnectionWebSocketCommand request, CancellationToken cancellationToken)
    {
        if(request.Context.Request.Headers.Authorization.FirstOrDefault() is var token)
        {
            logger.LogDebug("Попытка подключения с токеном {@Token}", token);
            var userModel = await auth.ValidateToken(new Token { Message = token ?? string.Empty }, cancellationToken);
            var user = userModel.MapToUserDto();
            if(!user.Id.Equals(Guid.Empty))
            {
                var userTmp = user with{ Id = Guid.Parse("bf6a36fd-87a2-49bc-955f-0fafc7712c70") };
                var socket = new WebSocketProvider(await request.Context.WebSockets.AcceptWebSocketAsync(), userTmp, config, request.Context);
                connections.AddConnection(socket);
                await sender.Send(new ConsumerWebSocketCommand(socket), cancellationToken);
                connections.Remove(socket);
            }
            else
                throw new ForbiddenAccessException("Токен не прошёл валидацию");
        }
    }
}