using Grpc.Core;
using Team_8.Contracts.DTOs;

namespace Domain.Interfaces;

public interface IUserContext
{
    UserDto User { get; set; }
    ServerCallContext Context { get; set; }
}