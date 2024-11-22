using Api.Extension;
using Application.Mediator.Commands.ConnectBattle;
using Application.Mediator.Commands.MakeStep;
using MediatR;
using ProtoBuf.Grpc;
using Team_8.Contracts.Protos.CodeFirst;

namespace Api.Services;

public class BattleService(ISender mediator) : IBattleService
{
    public async Task<CurrentMapStatusModel> Connect(ConnectBattleModel model, CallContext context = new()) =>
        await mediator.CommandAsync<CurrentMapStatusModel>(new ConnectBattleCommand(model), context.CancellationToken);

    public Task Disconnect(DisconnectBattleModel model, CallContext context = new()) => Task.CompletedTask;

    public async Task MakeStep(StepModel step, CallContext context = new()) =>
        await mediator.CommandAsync(new MakeStepCommand(step), context.CancellationToken);
}