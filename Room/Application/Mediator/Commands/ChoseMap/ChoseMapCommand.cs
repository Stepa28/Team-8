using Domain.Common.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Commands.ChoseMap;

public sealed record ChoseMapCommand(ChoseMapModel Model) : IRequest;

internal sealed class ChoseMapCommandHandler(ILogger<ChoseMapCommandHandler> logger, IRepository<Room> repositoryRoom, IRepository<Map> repositoryMap)
    : IRequestHandler<ChoseMapCommand>
{
    public async Task Handle(ChoseMapCommand request, CancellationToken cancellationToken)
    {
        var room = await repositoryRoom.GetAsync(request.Model.Room.Id, cancellationToken);
        var map = await repositoryMap.GetAsync(request.Model.Map.Id, cancellationToken);
        
        if(room == null)
            throw new NotFoundException($"Комнаты с Id({request.Model.Room.Id}) не существует");
        if(map == null)
            throw new NotFoundException($"Карты с Id({request.Model.Map.Id}) не существует");

        room.CurrentMap = map;
        await repositoryRoom.SaveChangedAsync(cancellationToken);
    }
}