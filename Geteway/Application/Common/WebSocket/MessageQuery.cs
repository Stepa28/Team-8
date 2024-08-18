using System.Collections.Concurrent;
using Application.Mediatr.Commands.ProcessingReceivedMessages;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.ConstStrings;

namespace Application.Common.WebSocket;

public class MessageQuery : IMessageQuery
{
    private readonly CancellationTokenSource _cancellationTokenMessageProcessor = new();
    private readonly ConcurrentQueue<(WebSocketProvider socket, string massage)> _messages = new();
    private readonly ILogger<MessageQuery> _logger;
    private readonly IServiceProvider _serviceProvider;

    public MessageQuery(ILogger<MessageQuery> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        StartWebSocketAsync();
    }

    public void AddMassage(WebSocketProvider socket, string massage)
    {
        if(massage != MessageConst.HeartbeatMessage)
            _messages.Enqueue((socket, massage));
    }

    private void StartWebSocketAsync()
    {
        _ = Task.Factory.StartNew(async () =>
        {
            _logger.LogInformation("Запуск обработчика сообщений: {@name}", nameof(MessageQuery));
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
                    var sender = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ISender>();
                    await sender.Send(new ProcessingReceivedMessagesCommand(message.socket, message.massage), cts.Token);
                }
                catch(Exception)
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