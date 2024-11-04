namespace Team_8.Contracts.DTOs;

public record CurrentUnitDto(Guid UserId, int UnitTypeId, int X, int Y, byte Hp);