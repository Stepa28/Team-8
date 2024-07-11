using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Team8.Contracts.Auth.Service;

namespace Infrastructure.Services;

public class AuthService(Team8.Contracts.Auth.Service.AuthService.AuthServiceClient client, ILogger<AuthService> logger)
    : IAuthService
{
    public async Task<string> LoginAsync(LoginModel model)
    {
        logger.LogInformation("Пользователь с Email: {@email} пытается залогиниться", model.Login);
        return (await client.LoginAsync(model)).Message;
    }

    public async Task<Guid> RegisterAsync(RegisterModel model)
    {
        return Guid.Parse((await client.RegisterAsync(model)).Value);
    }
}