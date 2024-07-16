using System.Collections.Concurrent;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Common.WebSocket;

public class WebSocketConnections : IWebSocketConnections
{
    private readonly ConcurrentDictionary<HttpContext, WebSocketProvider> _connectionByContext = new();
    private readonly ConcurrentDictionary<Guid, WebSocketProvider> _connectionByUserId = new();
    private readonly ConcurrentDictionary<WebSocketProvider, (HttpContext Context, Guid UserId)> _keysConnection = new();
    private readonly ISender _sender;

    public WebSocketConnections(ISender sender)
    {
        _sender = sender;
        Instances.Connections = this;
    }

    public void AddConnection(HttpContext context, WebSocketProvider socket)
    {
        _connectionByContext.TryAdd(context, socket);
        _connectionByUserId.TryAdd(socket.User.Id, socket);
        _keysConnection.TryAdd(socket, (context, socket.User.Id));
    }

    public void Remove(WebSocketProvider socket)
    {
        _keysConnection.TryGetValue(socket, out var keys);
        _connectionByContext.Remove(keys.Context, out _);
        _connectionByUserId.Remove(keys.UserId, out _);
        _keysConnection.Remove(socket, out _);
        socket.WebSocket.Dispose();
    }

    public WebSocketProvider? Connection(HttpContext context)
    {
        _connectionByContext.TryGetValue(context, out var value);
        return value;
    }
    
    public WebSocketProvider? Connection(Guid userId)
    {
        _connectionByUserId.TryGetValue(userId, out var value);
        return value;
    }
}