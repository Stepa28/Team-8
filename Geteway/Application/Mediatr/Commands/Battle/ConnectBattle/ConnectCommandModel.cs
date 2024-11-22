using Team_8.Contracts.Enums;
using Team_8.Contracts.Protos.CodeFirst;

namespace Application.Mediatr.Commands.Battle.ConnectBattle;

public sealed record ConnectCommandModel(int BattleId, List<UnitModel> Units, TilesModel Tileses, BattleState Status);