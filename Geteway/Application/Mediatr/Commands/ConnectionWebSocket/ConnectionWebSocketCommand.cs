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
            logger.LogInformation("Попытка подключения с токеном {@Token}", token);
            var userModel = await auth.ValidateToken(new Token { Message = token?.Split(' ').Last() ?? string.Empty }, cancellationToken);
            var user = userModel.MapToUserDto();
            if(!user.Id.Equals(Guid.Empty))
            {
                var socket = new WebSocketProvider(await request.Context.WebSockets.AcceptWebSocketAsync(), user, config, request.Context);
                connections.AddConnection(socket);
                await sender.Send(new ConsumerWebSocketCommand(socket), cancellationToken);
                connections.Remove(socket);
            }
            else
                throw new ForbiddenAccessException("Токен не прошёл валидацию");
        }
    }
}