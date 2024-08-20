using Team_8.Contracts.DTOs;

namespace Team_8.Contracts.MassTransitDto;

public record CreateBattleDto(int Id, TilesDto Tileses, List<UnitTypeDto> Units);