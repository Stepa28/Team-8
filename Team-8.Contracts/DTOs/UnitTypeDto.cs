using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record UnitTypeDto(Guid UserId, string Name, byte Damage, byte HP, byte Armor, UltimateType Ultimate, byte MovementSpeed);