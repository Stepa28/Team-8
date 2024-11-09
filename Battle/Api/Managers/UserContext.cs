using Domain.Interfaces;
using Grpc.Core;
using Team_8.Contracts.DTOs;

namespace Api.Managers;

public class UserContext : IUserContext
{
    public UserDto User { get; set; }
    public ServerCallContext Context { get; set; }
}