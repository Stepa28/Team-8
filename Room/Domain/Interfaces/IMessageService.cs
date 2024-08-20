using Team_8.Contracts.MassTransitDto;

namespace Domain.Interfaces;

public interface IMessageService
{
    Task ProcessBattleStatusMessage(BattleStatusDto dto, CancellationToken cancellationToken = default);
    Task ProcessUserAddMessage(AddUserDto dto, CancellationToken cancellationToken = default);
    Task ProcessUserDeleteMessage(DeleteUserDto dto, CancellationToken cancellationToken = default);
}