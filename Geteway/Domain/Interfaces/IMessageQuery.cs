using Domain.Common;

namespace Domain.Interfaces;

public interface IMessageQuery
{
    void AddMassage(WebSocketProvider socket, string massage);
}