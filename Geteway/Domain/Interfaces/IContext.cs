using Domain.Common;

namespace Domain.Interfaces;

public interface IContext
{
    WebSocketProvider? SocketProvider { get; set; }
}