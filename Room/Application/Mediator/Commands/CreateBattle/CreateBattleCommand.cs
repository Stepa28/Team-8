using Application.Common.Attributes;
using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Producers;
using Domain.Interfaces.Repository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Team_8.Contracts.DTOs;
using Team_8.Contracts.Enums;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Commands.CreateBattle;

[Authorize(Role.User)]
public sealed record CreateBattleCommand(RoomId Model) : IRequest;

internal sealed class CreateBattleCommandHandler(
    ILogger<CreateBattleCommandHandler> logger
    , IUserContext userContext
    , IRepository<Room> repository
    , IMapper mapper
    , ICreateBattleProducer producer)
    : IRequestHandler<CreateBattleCommand>
{
    public async Task Handle(CreateBattleCommand request, CancellationToken cancellationToken)
    {
        var room = await repository.GetAsync(request.Model.Id, cancellationToken);
        if(room == null)
            throw new NotFoundException($"Комнаты с Id({request.Model.Id}) не существует");
        var states = room.UserStates.Where(x => !x.IsDeleted).ToList();

        if(!states.Select(x => x.UserId).Contains(userContext.User.Id))
            throw new BadRequestException($"Вы не находитесь в комнате с Id {request.Model.Id}");
        if(!states.All(x => x.ReadyForBattle))
            throw new BadRequestException("Не все готовы к сражению");
        if(room.CurrentMapId == null)
            throw new BadRequestException($"У комнаты с Id = {request.Model.Id} не выбрана карта");
        if(!room.CreatorId.Equals(userContext.User.Id))
            throw new BadRequestException("Вы не создатель этой комнаты");

        room.RoomStatus = RoomStatus.InBattle;
        await repository.SaveChangedAsync(cancellationToken);

        var titleDto = mapper.Map<TilesDto>(room.CurrentMap);
        var battleDto = new CreateBattleDto { Id = room.Id, Tileses = titleDto };
        await producer.CreateBattle(battleDto, cancellationToken);
    }
}