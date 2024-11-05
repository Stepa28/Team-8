using Team_8.Contracts.DTOs;

namespace Domain.Interfaces.Services;

public interface IAuthService
{
    Task<string> GetToken(string email, string pass, CancellationToken cancellationToken = default);
    Task<UserDto> CheckValidToken(string token, CancellationToken cancellationToken = default);
    string GetHashPassword(string password);
}