using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Queries.GetRooms;

internal sealed class GetRoomsQueryHandler(ILogger<GetRoomsQueryHandler> logger, IRoomService service)
    : IRequestHandler<GetRoomsQuery, List<GetRoomsQueryModel>>
{
    public async Task<List<GetRoomsQueryModel>> Handle(GetRoomsQuery _, CancellationToken cancellationToken)
    {
        var res = await service.GetRooms(cancellationToken);
        return res.Models.MapToListGetRoomsQueryModel();
    }
}