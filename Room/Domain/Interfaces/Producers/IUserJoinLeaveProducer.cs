using Team_8.Contracts.MassTransitDto;

namespace Domain.Interfaces.Producers;

public interface IUserJoinLeaveProducer
{
    Task PushUserJoinLeave(UserJoinLeaveDto userDto, CancellationToken stoppingToken = default);
}