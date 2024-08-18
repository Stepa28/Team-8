using Riok.Mapperly.Abstractions;
using Team8.Contracts.Room.Service;

namespace Application.Mediatr.Queries.GetRooms;

[Mapper]
public static partial class GetRoomsQueryMapping
{
    public static partial List<GetRoomQueryModel> MapToListGetRoomsQueryModel(this IEnumerable<RoomModel> query);
}