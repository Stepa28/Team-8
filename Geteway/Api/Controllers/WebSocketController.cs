using Application.Mediatr.Commands.WebSocket.Connection;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class WebSocketController(ILogger<WebSocketController> logger) : BaseController
{
    [HttpGet("address-websocket")]
    public Task<string> GetWebSocketAddress()
    {
        return Task.FromResult($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{Reduce.WebSocketRoute}");
    }

    [HttpGet(Reduce.WebSocketRoute)]
    public async Task WebSocketConnect()
    {
        if(HttpContext.WebSockets.IsWebSocketRequest)
            await CommandAsync(new ConnectionWebSocketCommand(HttpContext));
        else
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    }
}