using AutoMapper;
using Domain.Models;
using Team_8.Contracts.DTOs;

namespace Application.Mediator.Commands.ChoseMap;

public class ConnectRoomCommandMapping : Profile
{
    public ConnectRoomCommandMapping()
    {
        CreateMap<Room, RoomInfoDto>();
    }
}