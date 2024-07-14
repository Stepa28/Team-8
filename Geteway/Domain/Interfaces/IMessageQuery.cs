using Domain.Common;

namespace Application.Common.WebSocket;

public interface IMessageQuery
{
    void AddMassage(WebSocketProvider socket, string massage);
}