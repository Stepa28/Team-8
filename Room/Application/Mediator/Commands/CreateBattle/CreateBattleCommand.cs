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
using Team_8.Contracts.MassTransitDto;
using Team8.Contracts.Room.Server;

namespace Application.Mediator.Commands.CreateBattle;

[Authorize(Role.User)]
public sealed record CreateBattleCommand(RoomId Model) : IRequest;

internal sealed class CreateBattleCommandHandler(
    ILogger<CreateBattleCommandHandler> logger,
    IUserContext userContext,
    IRepository<Room> repository,
    IRepository<UserState> userStateRepository,
    IRepository<Map> mapRepository,
    IMapper mapper,
    ICreateBattleProducer producer,
    IAddOrUpdateRoomProducer roomProducer)
    : IRequestHandler<CreateBattleCommand>
{
    public async Task Handle(CreateBattleCommand request, CancellationToken cancellationToken)
    {
        var room = await repository.GetAsync(request.Model.Id, cancellationToken);
        if(room == null)
            throw new NotFoundException($"Комнаты с Id({request.Model.Id}) не существует");
        var states = await userStateRepository.GetListAsync(cancellationToken, x => x.RoomId == room.Id && !x.IsDeleted);

        if(!states.Select(x => x.UserId).Contains(userContext.User.Id))
            throw new BadRequestException($"Вы не находитесь в комнате с Id {request.Model.Id}");
        if(!states.All(x => x.ReadyForBattle))
            throw new BadRequestException("Не все готовы к сражению");
        if(!states.All(x => x.UnitTypeId.HasValue))
            throw new BadRequestException("Не все выбрали тип подпонтрольного объекта");
        if(room.CurrentMapId == null)
            throw new BadRequestException($"У комнаты с Id = {request.Model.Id} не выбрана карта");
        if(!room.CreatorId.Equals(userContext.User.Id))
            throw new BadRequestException("Вы не создатель этой комнаты");

        room.RoomStatus = RoomStatus.InBattle;
        await repository.SaveChangedAsync(cancellationToken);

        var tilesDto = mapper.Map<TilesDto>(room.CurrentMap);
        logger.LogInformation("Плитки {@tiles}", tilesDto);
        List<UnitTypeDto> unitTypes = [];
        unitTypes.AddRange(states.Select(state => new UnitTypeDto(state.UserId, state.UnitType.Name, state.UnitType.Damage, state.UnitType.HP,
            state.UnitType.Armor, state.UnitType.Ultimate, state.UnitType.MovmentSpead)));

        var battleDto = new CreateBattleDto(room.Id, tilesDto, unitTypes);
        await producer.CreateBattle(battleDto, cancellationToken);

        var map = await mapRepository.GetAsync(x => !x.IsDeleted && x.Id == room.CurrentMapId, cancellationToken);
        var roomDto = new AddOrUpdateRoomDto(room.Id, room.Title, room.RoomStatus, map.Name, 0, userContext.User.Nick,
            RoomUpdateType.Status | RoomUpdateType.CurrentRound);
        await roomProducer.PushAddOrUpdateRoom(roomDto, cancellationToken);
    }
}