using Grpc.Core;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common;

public class BaseService
{
    public CallOptions getCallOptions(IConfiguration configuration, CancellationToken cancellationToken = default) =>
        new(null
            , DateTime.UtcNow.AddMinutes(int.Parse(configuration["gRPCDeadLineMinutes"]))
            , cancellationToken);
}