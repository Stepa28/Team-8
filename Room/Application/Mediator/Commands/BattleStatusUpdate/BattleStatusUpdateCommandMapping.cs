using AutoMapper;
using Domain.Models;
using Team_8.Contracts.MassTransitDto;

namespace Application.Mediator.Commands.BattleStatusUpdate;

public class BattleStatusUpdateCommandMapping : Profile
{
    public BattleStatusUpdateCommandMapping()
    {
        CreateMap<Room, AddOrUpdateRoomDto>();
    }
}