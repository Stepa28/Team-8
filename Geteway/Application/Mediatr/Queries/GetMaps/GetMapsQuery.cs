using MediatR;

namespace Application.Mediatr.Queries.GetMaps;

public sealed record GetMapsQuery : IRequest<GetMapsQueryModel>;