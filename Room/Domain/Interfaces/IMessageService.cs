using Team_8.Contracts.DTOs;

namespace Domain.Interfaces;

public interface IMessageService
{
    void ProcessBattleStatusMessage(BattleStatusDto dto, CancellationToken cancellationToken = default);
    void ProcessUserAddMessage(UserShortDto dto, CancellationToken cancellationToken = default);
    void ProcessUserDeleteMessage(UserShortDto dto, CancellationToken cancellationToken = default);
}