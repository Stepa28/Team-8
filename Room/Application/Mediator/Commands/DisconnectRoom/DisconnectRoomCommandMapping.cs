using AutoMapper;
using Team_8.Contracts.DTOs;

namespace Application.Mediator.Commands.DisconnectRoom;

public class DisconnectRoomCommandMapping : Profile
{
    public DisconnectRoomCommandMapping()
    {
        CreateMap<UserDto, UserJoinLeaveDto>()
            .ForMember(x => x.UserNick, expression => expression.MapFrom(x => x.Nick))
            .ForMember(x => x.UserId, expression => expression.MapFrom(x => x.Id));
    }
}