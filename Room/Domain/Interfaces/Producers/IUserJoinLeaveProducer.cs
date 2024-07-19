using Team_8.Contracts.DTOs;

namespace Domain.Interfaces.Producers;

public interface IUserJoinLeaveProducer
{
    Task CreateBattle(UserJoinLeaveDto userDto, CancellationToken stoppingToken);
}