using System.Text.Json;
using Domain.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Team_8.Contracts.ConstStrings;

namespace Infrastructure.Common;

public class BaseService(IContext context, IConfiguration configuration)
{
    protected CallOptions GetCallOptions(CancellationToken cancellationToken)
    {
        var header = new Metadata();
        if(context.SocketProvider != null)
            header.Add(RpcHeaders.UserContext, JsonSerializer.Serialize(context.SocketProvider.User));

        var deadLine = DateTime.UtcNow.AddMinutes(int.Parse(configuration["gRPCDeadLineMinutes"]));
        return new CallOptions(header, deadLine, cancellationToken);
    }
}