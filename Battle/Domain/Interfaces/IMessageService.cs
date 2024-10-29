using Team_8.Contracts.MassTransitDto;

namespace Domain.Interfaces;

public interface IMessageService
{
    Task ProcessCreateBattle(CreateBattleDto dto, CancellationToken cancellationToken = default);
}