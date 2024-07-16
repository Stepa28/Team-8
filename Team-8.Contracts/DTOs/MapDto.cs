using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record MapDto
{
    public string Name { get; set; }
    public TitleDto Titles { get; set; }
}