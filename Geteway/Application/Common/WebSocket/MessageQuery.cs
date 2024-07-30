using System.Collections.Concurrent;
using Application.Mediatr.Commands.ProcessingReceivedMessages;
using Domain.Common;
using Grpc.Core;
using MediatR;
using Serilog;
using Team_8.Contracts.ConstStrings;

namespace Application.Common.WebSocket;

public class MessageQuery
{
    private readonly CancellationTokenSource _cancellationTokenMessageProcessor = new();
    private readonly ConcurrentQueue<(WebSocketProvider socket, string massage)> _messages = new();
    private readonly ILogger _logger = Log.Logger.ForContext<MessageQuery>();
    private readonly ISender? _sender;
    private static MessageQuery? _this;

    public MessageQuery(ISender sender)
    {
        _sender ??= sender;
        StartWebSocketAsync();
    }

    private void AddMassage(WebSocketProvider socket, string massage)
    {
        if(massage != MessageConst.HeartbeatMessage)
            _messages.Enqueue((socket, massage));
    }

    public static void AddMassage(WebSocketProvider socket, string massage, ISender sender)
    {
        _this ??= new MessageQuery(sender);
        _this.AddMassage(socket, massage);
    }

    private void StartWebSocketAsync()
    {
        _ = Task.Factory.StartNew(async () =>
        {
            _logger.Information("Запуск обработчика сообщений: {@name}", nameof(MessageQuery));
            await MessageProcessorAsync();
        }, _cancellationTokenMessageProcessor.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
    }

    private async Task MessageProcessorAsync()
    {
        do
        {
            if(_messages.TryDequeue(out var message))
            {
                try
                {
                    var cts = new CancellationTokenSource();
                    await _sender.Send(new ProcessingReceivedMessagesCommand(message.socket, message.massage), cts.Token);
                }
                catch(RpcException)
                {
                    // ignored
                }
            }
            else
            {
                await Task.Delay(200, _cancellationTokenMessageProcessor.Token);
            }
        } while (!_cancellationTokenMessageProcessor.IsCancellationRequested);
    }
}