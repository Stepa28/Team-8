using Riok.Mapperly.Abstractions;
using Team8.Contracts.Room.Service;

namespace Application.Mediatr.Queries.Rooms.GetMaps;

[Mapper]
public static partial class GetMapsQueryMapping
{
    public static partial List<GetMapQueryModel> MapToListGetMapsQueryModel(this IEnumerable<MapShortModel> query);
}