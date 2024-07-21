using Team_8.Contracts.DTOs;

namespace Domain.Interfaces.Producers;

public interface IUserJoinLeaveProducer
{
    Task PushUserJoinLeave(UserJoinLeaveDto userDto, CancellationToken stoppingToken = default);
}