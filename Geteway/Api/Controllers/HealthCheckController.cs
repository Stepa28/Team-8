using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf.Grpc.Client;
using Serilog;
using Team_8.Contracts.Protos.CodeFirst;

namespace Api.Controllers;

public class HealthCheckController : BaseController
{
    private readonly Serilog.ILogger _logger = Log.ForContext<HealthCheckController>();

    [HttpGet("status")]
    public async Task<ActionResult> GetStatus()
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:50066");
        var client = channel.CreateGrpcService<IBattleService>();
        var tt = await client.MakeStep(new StepModel());
        
        var message = $"[{DateTime.Now:T}] Status: Ok!";
        _logger.Information(message);
        return Ok(message);
    }
}