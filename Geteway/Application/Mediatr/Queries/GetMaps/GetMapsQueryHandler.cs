using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Queries.GetMaps;

internal sealed class GetMapsQueryHandler(ILogger<GetMapsQueryHandler> logger, IRoomService service) : IRequestHandler<GetMapsQuery, List<GetMapsQueryModel>>
{
    public async Task<List<GetMapsQueryModel>> Handle(GetMapsQuery request, CancellationToken cancellationToken)
    {
        var res = await service.GetMaps(cancellationToken);
        return res.Models.MapToListGetMapsQueryModel();
    }
}