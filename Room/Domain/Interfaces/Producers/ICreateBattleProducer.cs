using Team_8.Contracts.MassTransitDto;

namespace Domain.Interfaces.Producers;

public interface ICreateBattleProducer
{
    Task CreateBattle(CreateBattleDto battleDto, CancellationToken stoppingToken = default);
}