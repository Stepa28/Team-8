using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Api.Controllers;

public class WebSocketController : BaseController
{
    private readonly ILogger _logger = Log.ForContext<WebSocketController>();
    private static List<WebSocket> _conections = [];

    [NonAction]
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            _logger.Information("Подключились с токеном {@auth}", HttpContext.Request.Headers.Authorization.First());
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await Echo(webSocket);
            //return Ok();
        }
        else
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    }
    
    private static async Task Echo(WebSocket webSocket)
    {
        _conections.Add(webSocket);
        var buffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!receiveResult.CloseStatus.HasValue)
        {
            var str = Encoding.Default.GetString(buffer);
            foreach (var soket in _conections)
            {
                await soket.SendAsync(
                                new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                                receiveResult.MessageType,
                                receiveResult.EndOfMessage,
                                CancellationToken.None);
            }
            

            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
        _conections.Remove(webSocket);
    }
}