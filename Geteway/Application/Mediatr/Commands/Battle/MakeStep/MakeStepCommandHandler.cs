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

namespace Application.Mediatr.Commands.Battle.MakeStep;

internal sealed class MakeStepCommandHandler(
    IOptions<GrpsOptions> options,
    IContext context,
    ILogger<MakeStepCommandHandler> logger) : IRequestHandler<MakeStepCommand>
{
    public async Task Handle(MakeStepCommand request, CancellationToken cancellationToken)
    {
        var op = options.Value;
        using var connect = GrpcChannel.ForAddress($"{op.BattleServer}");
        var client = connect.CreateGrpcService<IBattleService>();

        logger.LogInformation(JsonSerializer.Serialize(request));
        var header = new Metadata();
        if(context.SocketProvider != null)
            header.Add(RpcHeaders.UserContext, JsonSerializer.Serialize(context.SocketProvider.User));
        var deadLine = DateTime.UtcNow.AddMinutes(op.DeadLineMinutes);

        await client.MakeStep(request.MapToStepModel(), new CallOptions(header, deadLine, cancellationToken));
    }
}