using System.Collections.Concurrent;
using Domain.Common;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Common.WebSocket;

public class WebSocketConnections : IWebSocketConnections
{
    private readonly ConcurrentDictionary<HttpContext, WebSocketProvider> _connectionByContext = new();
    private readonly ConcurrentDictionary<Guid, WebSocketProvider> _connectionByUserId = new();

    public WebSocketConnections()
    {
        Instances.Connections = this;
    }

    public void AddConnection(WebSocketProvider socket)
    {
        _connectionByContext.TryAdd(socket.Context, socket);
        _connectionByUserId.TryAdd(socket.User.Id, socket);
    }

    public void Remove(WebSocketProvider socket)
    {
        _connectionByContext.Remove(socket.Context, out _);
        _connectionByUserId.Remove(socket.User.Id, out _);
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

    public List<WebSocketProvider> ActiveConnections()
    {
        return _connectionByContext.Values.ToList();
    }

    public WebSocketProvider? ConnectionsFromUser(Guid id)
    {
        _connectionByUserId.TryGetValue(id,  out var result);
        return result;
    }
}