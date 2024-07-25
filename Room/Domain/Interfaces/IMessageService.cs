using Team_8.Contracts.DTOs;

namespace Domain.Interfaces;

public interface IMessageService
{
    Task ProcessBattleStatusMessage(BattleStatusDto dto, CancellationToken cancellationToken = default);
    Task ProcessUserAddMessage(UserShortDto dto, CancellationToken cancellationToken = default);
    Task ProcessUserDeleteMessage(UserShortDto dto, CancellationToken cancellationToken = default);
}