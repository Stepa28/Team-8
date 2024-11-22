using Team_8.Contracts.DTOs;

namespace Domain.Interfaces.Producers;

public interface IRemoveRoomProducer
{
    Task PushRemoveRoom(RoomInfoDto roomDto, CancellationToken stoppingToken = default);
}