using System.Net.WebSockets;

namespace Domain.Common.Exceptions;

public class SocketCloseConnectException(string message, WebSocketCloseStatus closeStatus) : Exception(message)
{
    public WebSocketCloseStatus CloseStatus { get; private set; } = closeStatus;
}