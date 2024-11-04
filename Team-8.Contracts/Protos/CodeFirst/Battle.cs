using System.Runtime.Serialization;
using System.ServiceModel;
using ProtoBuf.Grpc;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.Enums;

namespace Team_8.Contracts.Protos.CodeFirst;

[DataContract]
public class ConnectBattleModel
{
    [DataMember(Order = 1)] public int Id { get; set; }
}

[DataContract]
public class DisconnectBattleModel
{
    [DataMember(Order = 1)] public int Id { get; set; }
}

[DataContract]
public class UnitModel
{
    [DataMember(Order = 1)] public Guid PlayerId { get; set; }
    [DataMember(Order = 2)] public int X { get; set; }
    [DataMember(Order = 3)] public int Y { get; set; }
    [DataMember(Order = 4)] public int Health { get; set; }
    [DataMember(Order = 5)] public int UnitType { get; set; }
}

[DataContract]
public class TilesModel
{
    [DataMember(Order = 1)] public List<TilesType> TilesType { get; set; }
    [DataMember(Order = 2)] public int CountRow { get; set; }
    [DataMember(Order = 3)] public int CountColumn { get; set; }
}

[DataContract]
public class CurrentMapStatusModel
{
    [DataMember(Order = 1)] public List<UnitModel> Units { get; set; }
    [DataMember(Order = 2)] public TilesModel Tileses { get; set; }
    [DataMember(Order = 3)] public BattleState Status { get; set; }
}

[DataContract]
public class StepModel
{
    [DataMember(Order = 1)] public int BattleId { get; set; }
    [DataMember(Order = 2)] public Routes Move { get; set; }
    [DataMember(Order = 3)] public Routes Attack { get; set; }
}

[ServiceContract]
public interface IBattleService
{
    [OperationContract]
    Task<CurrentMapStatusModel> Connect(ConnectBattleModel model, CallContext context = default);

    [OperationContract]
    Task Disconnect(DisconnectBattleModel model, CallContext context = default);

    [OperationContract]
    Task MakeStep(StepModel step, CallContext context = default);
}