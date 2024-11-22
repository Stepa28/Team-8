using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record TilesDto(List<TilesType> TilesType, int CountRow, int CountColumn);