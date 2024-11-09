using Team_8.Contracts.MassTransitDto;

namespace Domain.Interfaces;

public interface IMessageService
{
    Task ProcessAddOrUpdateRoomMessage(AddOrUpdateRoomDto dto, CancellationToken cancellationToken = default);
    Task ProcessBattleStateMessage(BattleStateDto dto, CancellationToken cancellationToken = default);
}