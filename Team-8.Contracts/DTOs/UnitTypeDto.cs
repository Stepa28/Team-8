using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record UnitTypeDto
{
    public UserShortDto User { get; set; }
    public string Name { get; set; }
    public byte Damage { get; set; }
    public byte HP { get; set; }
    public byte Armor { get; set; }
    public UltimateType Ultimate { get; set; }
    public byte MovmentSpead { get; set; }
}