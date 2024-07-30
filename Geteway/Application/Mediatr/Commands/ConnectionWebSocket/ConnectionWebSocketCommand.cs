using Application.Mediatr.Commands.ConsumerWebSocket;
using AutoMapper;
using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.DTOs;
using Team8.Contracts.Auth.Service;

namespace Application.Mediatr.Commands.ConnectionWebSocket;

public sealed record ConnectionWebSocketCommand(HttpContext Context) : IRequest;

internal sealed class ConnectionWebSocketCommandHandler(
    ILogger<ConnectionWebSocketCommandHandler> logger
    , IWebSocketConnections connections
    , IAuthService auth
    , ISender sender
    , IMapper mapper
    , IConfiguration config) : IRequestHandler<ConnectionWebSocketCommand>
{
    public async Task Handle(ConnectionWebSocketCommand request, CancellationToken cancellationToken)
    {
        if(request.Context.Request.Headers.Authorization.FirstOrDefault() is var token)
        {
            logger.LogDebug("Попытка подключения с токеном {@Token}", token);
            var userModel = await auth.ValidateToken(new Token { Message = token ?? string.Empty }, cancellationToken);
            var user = mapper.Map<UserDto>(userModel);
            if(!user.Id.Equals(Guid.Empty))
            {
                var socket = new WebSocketProvider(await request.Context.WebSockets.AcceptWebSocketAsync(), user, config);
                connections.AddConnection(request.Context, socket);
                await sender.Send(new ConsumerWebSocketCommand(socket), cancellationToken);
                connections.Remove(socket);
            }
            else
                throw new ForbiddenAccessException("Токен не прошёл валидацию");
        }
    }
}