using Domain.Common;
using Domain.Enums;
using Team_8.Contracts.Enums;

namespace Domain.Models;

public record Room : BaseEntitySoftDelete<Room>
{
    public string Title { get; set; }
    public string HashPass { get; set; }
    public RoomStatus RoomStatus { get; set; }
    public virtual Map CurrentMap { get; set; }
    public int CurrentRound { get; set; }

    public override void Update(Room entity)
    {
        Title = entity.Title;
        HashPass = entity.HashPass;
        RoomStatus = entity.RoomStatus;
        CurrentMap = entity.CurrentMap;
        CurrentRound = entity.CurrentRound;
    }
}