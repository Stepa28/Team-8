using AutoMapper;
using Domain.Models;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Queries.GetRooms;

public class GetRoomsQueryMapping : Profile
{
    public GetRoomsQueryMapping()
    {
        CreateMap<Room, RoomsModel>();
    }
}