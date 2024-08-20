using Team_8.Contracts.Enums;

namespace Team_8.Contracts.MassTransitDto;

public record BattleStatusDto(int Id, int RoundCurrent, BattleState State);