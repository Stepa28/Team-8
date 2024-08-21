using Domain.Common;
using MediatR;

namespace Application.Mediatr.Commands.ProcessingReceivedMessages;

public sealed record ProcessingReceivedMessagesCommand(WebSocketProvider Socket, string Message) : IRequest;