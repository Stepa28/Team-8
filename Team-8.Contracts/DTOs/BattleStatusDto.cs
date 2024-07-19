using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record BattleStatusDto
{
    public int Id { get; set; }
    public int RoundCurrent { get; set; }
    public BattleState State { get; set; }
}