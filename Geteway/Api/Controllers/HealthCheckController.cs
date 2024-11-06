using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Api.Controllers;

public class HealthCheckController : BaseController
{
    private readonly Serilog.ILogger _logger = Log.ForContext<HealthCheckController>();

    [HttpGet("status")]
    public ActionResult GetStatus()
    {
        var message = $"[{DateTime.Now:T}] Status: Ok!";
        _logger.Information(message);
        return Ok(message);
    }
}