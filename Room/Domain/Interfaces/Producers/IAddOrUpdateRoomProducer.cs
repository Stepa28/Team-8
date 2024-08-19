using Team_8.Contracts.DTOs;

namespace Domain.Interfaces.Producers;

public interface IAddOrUpdateRoomProducer
{
    Task PushAddOrUpdateRoom(RoomInfoDto roomDto, CancellationToken stoppingToken = default);
}