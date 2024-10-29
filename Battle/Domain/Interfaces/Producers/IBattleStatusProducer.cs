using Team_8.Contracts.MassTransitDto;

namespace Domain.Interfaces.Producers;

public interface IBattleStatusProducer
{
    Task PushUpdateBattleStatus(BattleStatusDto battleStatus, CancellationToken stoppingToken = default);
}