using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record BattleStatusDto(int Id, int RoundCurrent, BattleState State);