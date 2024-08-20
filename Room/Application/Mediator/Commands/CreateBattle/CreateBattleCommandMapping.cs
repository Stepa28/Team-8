using AutoMapper;
using Domain.Models;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.MassTransitDto;

namespace Application.Mediator.Commands.CreateBattle;

public class CreateBattleCommandMapping : Profile
{
    public CreateBattleCommandMapping()
    {
        CreateMap<Map, TilesDto>();
        CreateMap<UnitType, UnitTypeDto>();
        CreateMap<Room, AddOrUpdateRoomDto>();
    }
}