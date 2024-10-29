using Api.Extension;
using Application.Mediator.Commands.ConnectBattle;
using MediatR;
using ProtoBuf.Grpc;
using Team_8.Contracts.Protos.CodeFirst;

namespace Api.Services;

public class BattleService(ISender mediator) : IBattleService
{
    public async Task<CurrentMapStatusModel> Connect(ConnectBattleModel model, CallContext context = new())
    {
        return await mediator.CommandAsync<CurrentMapStatusModel>(new ConnectBattleCommand(model), context.CancellationToken);
    }

    public Task Disconnect(DisconnectBattleModel model, CallContext context = new())
    {
        return Task.CompletedTask;
    }

    public Task<CurrentMapStatusModel> MakeStep(StepModel step, CallContext context = new())
    {
        return Task.FromResult(new CurrentMapStatusModel());
    }
}