using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Queries.GetRooms;

public sealed record GetRoomsQuery : IRequest<RoomsModel>;

internal sealed class GetRoomsQueryHandler(ILogger<GetRoomsQueryHandler> logger, IRepository<Room> roomRepo, IMapper mapper, IUserContext context)
    : IRequestHandler<GetRoomsQuery, RoomsModel>
{
    public async Task<RoomsModel> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation(context.User.Nick);
        var rooms = await roomRepo.GetListAsync(cancellationToken);
        var roomsModel = mapper.Map<List<RoomModel>>(rooms);
        var result = new RoomsModel();
        result.Models.AddRange(roomsModel);
        
        return result;
    }
}