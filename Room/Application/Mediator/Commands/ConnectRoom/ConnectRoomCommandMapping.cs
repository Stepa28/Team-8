using AutoMapper;
using Domain.Models;
using Team_8.Contracts.DTOs;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Commands.ConnectRoom;

public class ConnectRoomCommandMapping : Profile
{
    public ConnectRoomCommandMapping()
    {
        CreateMap<UserDto, UserJoinLeaveDto>()
            .ForMember(x => x.UserNick, expression => expression.MapFrom(x => x.Nick))
            .ForMember(x => x.UserId, expression => expression.MapFrom(x => x.Id));

        CreateMap<Room, ConnectRoomCommandModel>()
            .ForMember(x => x.NameCurrentMap, expression => expression.MapFrom(y => (y.CurrentMap ?? new Map { Name = "" }).Name));
        CreateMap<ConnectRoomCommandModel, RoomModel>();
    }
}