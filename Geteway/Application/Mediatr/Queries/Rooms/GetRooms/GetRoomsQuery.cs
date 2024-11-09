using MediatR;

namespace Application.Mediatr.Queries.Rooms.GetRooms;

public sealed record GetRoomsQuery : IRequest<GetRoomsQueryModel>;