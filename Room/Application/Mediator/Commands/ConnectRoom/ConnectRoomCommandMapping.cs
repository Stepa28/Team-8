using AutoMapper;
using Domain.Models;
using Team_8.Contracts.DTOs;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Commands.ConnectRoom;

public class ConnectRoomCommandMapping : Profile
{
    public ConnectRoomCommandMapping()
    {
        CreateMap<UserJoinLeaveDto, UserDto>()
            .ForMember(x => x.Nick, expression => expression.MapFrom(x => x.UserNick))
            .ForMember(x => x.Id, expression => expression.MapFrom(x => x.UserId));

        CreateMap<RoomModel, Room>()
            .ForMember(x => (x.CurrentMap ?? new Map { Name = "" }).Name, expression => expression.MapFrom(y => y.NameCurrentMap));
    }
}