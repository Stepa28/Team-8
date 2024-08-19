using AutoMapper;
using Domain.Models;
using Team_8.Contracts.DTOs;

namespace Application.Mediator.Commands.BattleStatusUpdate;

public class BattleStatusUpdateCommandMapping : Profile
{
    public BattleStatusUpdateCommandMapping()
    {
        CreateMap<Room, RoomInfoDto>();
    }
}