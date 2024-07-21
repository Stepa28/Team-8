using AutoMapper;
using Team_8.Contracts.DTOs;

namespace Application.Mediator.Commands.DisconnectRoom;

public class DisconnectRoomCommandMapping : Profile
{
    public DisconnectRoomCommandMapping()
    {
        CreateMap<UserJoinLeaveDto, UserDto>()
            .ForMember(x=>x.Nick, expression => expression.MapFrom(x=>x.UserNick))
            .ForMember(x=>x.Id, expression => expression.MapFrom(x=>x.UserId));
    }
}