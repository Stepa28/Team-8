using Team_8.Contracts.MassTransitDto;

namespace Domain.Interfaces.Producers;

public interface IAddOrUpdateRoomProducer
{
    Task PushAddOrUpdateRoom(AddOrUpdateRoomDto roomDto, CancellationToken stoppingToken = default);
}