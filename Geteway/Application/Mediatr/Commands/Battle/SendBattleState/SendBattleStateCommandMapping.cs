using Riok.Mapperly.Abstractions;
using Team_8.Contracts.MassTransitDto;
using Team_8.Contracts.UserTransfer;

namespace Application.Mediatr.Commands.Battle.SendBattleState;

[Mapper]
public static partial class SendBattleStateCommandMapping
{
    public static partial BattleStateUpdateDto MapToBattleStateUpdateDto(this BattleStateDto query);
}