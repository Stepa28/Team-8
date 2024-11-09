using System.Text.Json;
using Domain.Common.Configuration;
using Domain.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Options;
using Team_8.Contracts.ConstStrings;

namespace Infrastructure.Common;

public class BaseService(IContext context, IOptions<GrpsOptions> options)
{
    protected CallOptions GetCallOptions(CancellationToken cancellationToken)
    {
        var header = new Metadata();
        if(context.SocketProvider != null)
            header.Add(RpcHeaders.UserContext, JsonSerializer.Serialize(context.SocketProvider.User));

        var deadLine = DateTime.UtcNow.AddMinutes(options.Value.DeadLineMinutes);
        return new CallOptions(header, deadLine, cancellationToken);
    }
}