using Team_8.Contracts.MassTransitDto;

namespace Domain.Interfaces.Producers;

public interface IUserAddProducer
{
    Task PushAddUser(AddUserDto roomDto, CancellationToken stoppingToken = default);
}