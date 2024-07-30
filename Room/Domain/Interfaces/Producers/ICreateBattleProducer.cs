using Team_8.Contracts.DTOs;

namespace Domain.Interfaces.Producers;

public interface ICreateBattleProducer
{
    Task CreateBattle(CreateBattleDto battleDto, CancellationToken stoppingToken = default);
}