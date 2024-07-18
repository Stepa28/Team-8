using Domain.Common;
using Domain.Enums;

namespace Domain.Models;

public record Room : BaseEntitySoftDelete<Room>
{
    public string Title { get; set; }
    public string HashPass { get; set; }
    public Status Status { get; set; }
    public virtual Map CurrentMap { get; set; }
    public int CurrentRound { get; set; }

    public override void Update(Room entity)
    {
        Title = entity.Title;
        HashPass = entity.HashPass;
        Status = entity.Status;
        CurrentMap = entity.CurrentMap;
        CurrentRound = entity.CurrentRound;
    }
}