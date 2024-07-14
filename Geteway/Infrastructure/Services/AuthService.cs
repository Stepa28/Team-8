using Domain.Interfaces;
using Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Team8.Contracts.Auth.Service;

namespace Infrastructure.Services;

public class AuthService(Team8.Contracts.Auth.Service.AuthService.AuthServiceClient client, ILogger<AuthService> logger, IConfiguration configuration)
    : BaseService, IAuthService
{
    public async Task<string> LoginAsync(LoginModel model, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Пользователь с Email: {@email} пытается залогиниться", model.Login);
        return (await client.LoginAsync(model, getCallOptions(configuration, cancellationToken))).Message;
    }

    public async Task<Guid> RegisterAsync(RegisterModel model, CancellationToken cancellationToken = default)
    {
        return Guid.Parse((await client.RegisterAsync(model, getCallOptions(configuration, cancellationToken))).Value);
    }

    public async Task<UserModel> ValidateToken(Token token, CancellationToken cancellationToken = default)
    {
        var result = await client.ValidateTokenAsync(token, getCallOptions(configuration, cancellationToken));
        return result;
    }
}