using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record TitleDto()
{
    public List<TitleType> TitleType { get; set; }
    public int CountRow { get; set; }
    public int CountColumn { get; set; }
};