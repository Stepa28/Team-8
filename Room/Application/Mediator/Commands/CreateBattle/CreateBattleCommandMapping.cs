using AutoMapper;
using Domain.Models;
using Team_8.Contracts.DTOs;

namespace Application.Mediator.Commands.CreateBattle;

public class CreateBattleCommandMapping : Profile
{
    public CreateBattleCommandMapping()
    {
        CreateMap<Map, TilesDto>();
    }
}