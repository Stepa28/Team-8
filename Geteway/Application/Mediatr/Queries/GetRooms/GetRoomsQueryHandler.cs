using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Mediatr.Queries.GetRooms;

internal sealed class GetRoomsQueryHandler(ILogger<GetRoomsQueryHandler> logger, IRoomService service)
    : IRequestHandler<GetRoomsQuery, GetRoomsQueryModel>
{
    public async Task<GetRoomsQueryModel> Handle(GetRoomsQuery _, CancellationToken cancellationToken)
    {
        var res = await service.GetRooms(cancellationToken);
        return new GetRoomsQueryModel(res.Models.MapToListGetRoomsQueryModel());
    }
}