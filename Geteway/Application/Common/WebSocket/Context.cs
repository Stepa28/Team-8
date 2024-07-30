using Domain.Common;
using Domain.Interfaces;

namespace Application.Common.WebSocket;

public class Context : IContext
{
    public WebSocketProvider? SocketProvider { get; set; }
}