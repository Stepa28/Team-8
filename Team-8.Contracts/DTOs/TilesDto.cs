using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record TilesDto
{
    public List<TilesType> TilesType { get; set; }
    public int CountRow { get; set; }
    public int CountColumn { get; set; }
};