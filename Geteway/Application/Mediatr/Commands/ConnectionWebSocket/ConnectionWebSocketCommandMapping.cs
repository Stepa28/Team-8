using AutoMapper;
using Team_8.Contracts.DTOs;
using Team8.Contracts.Auth.Service;

namespace Application.Mediatr.Commands.ConnectionWebSocket;

public class ConnectionWebSocketCommandMapping : Profile
{
    public ConnectionWebSocketCommandMapping()
    {
        CreateMap<UserModel, UserDto>().ForMember(u => u.Id, model => model.MapFrom(x => Guid.Parse(x.Id.Value)));
    }
}