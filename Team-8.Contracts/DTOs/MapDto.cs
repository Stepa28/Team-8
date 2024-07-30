using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record MapDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TilesDto Tileses { get; set; }
}