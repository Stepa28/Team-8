using MediatR;

namespace Application.Mediatr.Queries.Rooms.GetMaps;

public sealed record GetMapsQuery : IRequest<GetMapsQueryModel>;