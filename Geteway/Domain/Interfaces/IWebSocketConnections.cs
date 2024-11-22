using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces;

public interface IWebSocketConnections
{
    void AddConnection(WebSocketProvider socket);
    void Remove(WebSocketProvider socket);
    WebSocketProvider? Connection(HttpContext context);
    WebSocketProvider? Connection(Guid userId);
    List<WebSocketProvider> ActiveConnections();
    WebSocketProvider? ConnectionsFromUser(Guid id);
}