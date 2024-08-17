namespace Team_8.Contracts.DTOs;

public record CreateBattleDto(int Id, TilesDto Tileses, List<UnitTypeDto> Units);