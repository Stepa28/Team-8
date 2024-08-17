using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record UnitTypeDto(UserShortDto User, string Name, byte Damage, byte HP, byte Armor, UltimateType Ultimate, byte MovementSpeed);