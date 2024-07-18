using Domain.Common;
using Team_8.Contracts.Enums;

namespace Domain.Models;

public record Map : BaseEntitySoftDelete<Map>
{
    public string Name { get; set; }
    public List<TitleType> TitleType { get; set; }
    public int CountRow { get; set; }
    public int CountColumn { get; set; }

    public override void Update(Map entity)
    {
        Name = entity.Name;
        TitleType = entity.TitleType;
        CountColumn = entity.CountColumn;
        CountRow = entity.CountRow;
    }
}