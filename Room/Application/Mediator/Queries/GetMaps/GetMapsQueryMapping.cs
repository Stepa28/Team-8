using AutoMapper;
using Domain.Models;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Queries.GetMaps;

public class GetMapsQueryMapping : Profile
{
    public GetMapsQueryMapping()
    {
        CreateMap<Map, MapShortModel>()
            .ForMember(x => x.Column, expression => expression.MapFrom(y => y.CountColumn))
            .ForMember(x => x.Row, expression => expression.MapFrom(y => y.CountColumn));
    }
}