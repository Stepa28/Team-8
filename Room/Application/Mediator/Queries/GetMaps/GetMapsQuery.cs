using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Queries.GetMaps;

public sealed record GetMapsQuery : IRequest<MapsShortModel>;

internal sealed class GetMapsQueryHandler(ILogger<GetMapsQueryHandler> logger, IRepository<Map> repository, IMapper mapper)
    : IRequestHandler<GetMapsQuery, MapsShortModel>
{
    public async Task<MapsShortModel> Handle(GetMapsQuery request, CancellationToken cancellationToken)
    {
        var maps = await repository.GetListAsync(cancellationToken);
        var mapsShort = mapper.Map<List<MapShortModel>>(maps);
        
        var result = new MapsShortModel();
        result.Models.AddRange(mapsShort);
        
        return result;
    }
}