using Riok.Mapperly.Abstractions;
using Team_8.Contracts.Protos.CodeFirst;

namespace Application.Mediatr.Commands.Battle.ConnectBattle;

[Mapper]
public static partial class ConnectBattleCommandMapping
{
    public static partial ConnectCommandModel MapToConnectBattleCommandModel(this CurrentMapStatusModel query);
}