using MediatR;

namespace Application.Mediatr.Queries.GetRooms;

public sealed record GetRoomsQuery : IRequest<GetRoomsQueryModel>;