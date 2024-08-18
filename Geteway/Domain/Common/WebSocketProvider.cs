using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Domain.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Team_8.Contracts.DTOs;

namespace Domain.Common;

public class WebSocketProvider(WebSocket webSocket, UserDto user, IConfiguration conf)
{
    public WebSocket WebSocket => webSocket;
    public UserDto User => user;

    public async Task<string> ReceiveMessageAsync(CancellationToken cancellationToken)
    {
        var bufferSize = 1024;

        var offset = 0;
        var packet = bufferSize;
        var buffer = new byte[bufferSize];
        var stringBuilder = new StringBuilder();
        var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromMinutes(int.Parse(conf["NumberOfMinutesOfWebSocketDowntimeCloseTheConnection"] ?? "1")));
        WebSocketReceiveResult response;
        do
        {
            var receive = new ArraySegment<byte>(buffer, offset, packet);
            response = await WebSocket.ReceiveAsync(receive, cts.Token);
            var answer = Encoding.ASCII.GetString(buffer, offset, response.Count);
            stringBuilder.Append(answer);
        } while (!response.EndOfMessage && !cancellationToken.IsCancellationRequested && !response.CloseStatus.HasValue);

        if(response.CloseStatus.HasValue)
            throw new SocketCloseConnectException(response.CloseStatusDescription ?? string.Empty, response.CloseStatus.Value);

        return $"{stringBuilder}";
    }
    
    public async Task SendMessageAsync<T>(T model, CancellationToken token = default)
    {
        await SendMessageAsync(ModelToJson(model), token);
    }
    
    public async Task SendMessageAsync(string message, CancellationToken token = default)
    {
        var bytes = Encoding.UTF8.GetBytes(message);
        var arraySegment = new ArraySegment<byte>(bytes);
        await WebSocket.SendAsync
        (
            arraySegment,
            WebSocketMessageType.Text,
            WebSocketMessageFlags.EndOfMessage,
            token
        );
    }

    public async Task CloseConnection(string message, WebSocketCloseStatus closeStatus, CancellationToken token)
    {
        await WebSocket.CloseAsync(closeStatus, message, token);
    }

    private static string ModelToJson<TModel>(TModel model)
    {
        try { return JsonSerializer.Serialize(model); }
        catch (NotSupportedException ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
}