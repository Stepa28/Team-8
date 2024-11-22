using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Queries.Rooms.GetMaps;

internal sealed class GetMapsQueryHandler(ILogger<GetMapsQueryHandler> logger, IRoomService service) : IRequestHandler<GetMapsQuery, GetMapsQueryModel>
{
    public async Task<GetMapsQueryModel> Handle(GetMapsQuery request, CancellationToken cancellationToken)
    {
        var res = await service.GetMaps(cancellationToken);
        return new GetMapsQueryModel(res.Models.MapToListGetMapsQueryModel());
    }
}