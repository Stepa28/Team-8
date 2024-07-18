using Domain.Common;
using Domain.Enums;

namespace Domain.Models;

public record UnitType : BaseEntitySoftDelete<UnitType>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public byte Damage { get; set; }
    public byte HP { get; set; }
    public byte Armor { get; set; }
    public UltimateType Ultimate { get; set; }
    public byte MovmentSpead { get; set; }

    public override void Update(UnitType entity)
    {
        Name = entity.Name;
        Description = entity.Description;
        Damage = entity.Damage;
        HP = entity.HP;
        Armor = entity.Armor;
        Ultimate = entity.Ultimate;
        MovmentSpead = entity.MovmentSpead;
    }
}