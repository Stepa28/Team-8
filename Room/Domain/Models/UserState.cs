using Domain.Common;

namespace Domain.Models;

public record UserState : BaseEntitySoftDelete<UserState>
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public int RoomId { get; set; }
    public virtual Room Room { get; set; }
    public int? UnitTypeId { get; set; }
    public virtual UnitType? UnitType { get; set; }
    public bool ReadyForBattle { get; set; }

    public override void Update(UserState entity)
    {
        User = entity.User;
        Room = entity.Room;
        UnitType = entity.UnitType;
        ReadyForBattle = entity.ReadyForBattle;
    }
}