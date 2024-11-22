using Team_8.Contracts.MassTransitDto;

namespace Domain.Interfaces.Producers;

public interface IBattleStateProducer
{
    Task PushUpdateBattleState(BattleStateDto battleStatus, CancellationToken stoppingToken = default);
}