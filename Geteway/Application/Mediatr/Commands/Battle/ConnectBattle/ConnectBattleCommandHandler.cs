using System.Text.Json;
using Domain.Common.Configuration;
using Domain.Interfaces;
using Grpc.Core;
using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProtoBuf.Grpc.Client;
using Team_8.Contracts.ConstStrings;
using Team_8.Contracts.Protos.CodeFirst;

namespace Application.Mediatr.Commands.Battle.ConnectBattle;

internal sealed class ConnectBattleCommandHandler(
    IOptions<GrpsOptions> options,
    IContext context,
    ILogger<ConnectBattleCommandHandler> logger)
    : IRequestHandler<ConnectCommand, ConnectCommandModel>
{
    public async Task<ConnectCommandModel> Handle(ConnectCommand request, CancellationToken cancellationToken)
    {
        var op = options.Value;
        using var connect = GrpcChannel.ForAddress($"{op.BattleServer}");
        var client = connect.CreateGrpcService<IBattleService>();

        logger.LogInformation(request.Id.ToString());
        var header = new Metadata();
        if(context.SocketProvider != null)
            header.Add(RpcHeaders.UserContext, JsonSerializer.Serialize(context.SocketProvider.User));
        var deadLine = DateTime.UtcNow.AddMinutes(op.DeadLineMinutes);

        var result = await client.Connect(new ConnectBattleModel { Id = request.Id }, new CallOptions(header, deadLine, cancellationToken));
        logger.LogInformation(JsonSerializer.Serialize(result));
        return result.MapToConnectBattleCommandModel();
    }
}