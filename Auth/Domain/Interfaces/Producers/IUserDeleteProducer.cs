using Team_8.Contracts.MassTransitDto;

namespace Domain.Interfaces.Producers;

public interface IUserDeleteProducer
{
    Task PushDeleteUser(DeleteUserDto userDto, CancellationToken stoppingToken = default);
}