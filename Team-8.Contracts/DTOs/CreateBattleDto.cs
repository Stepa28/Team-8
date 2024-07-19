namespace Team_8.Contracts.DTOs;

public record CreateBattleDto
{
    public int Id { get; set; }
    public TitleDto Titles { get; set; }
    public List<UnitTypeDto> Units { get; set; } = [];
}