using Domain.Common.Configuration;
using Domain.Interfaces;
using Infrastructure.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Team8.Contracts.Auth.Service;

namespace Infrastructure.Services;

public class AuthService(
    Team8.Contracts.Auth.Service.AuthService.AuthServiceClient client,
    ILogger<AuthService> logger,
    IContext context,
    IOptions<GrpsOptions> options)
    : BaseService(context, options), IAuthService
{
    public async Task<string> LoginAsync(LoginModel model, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Пользователь с Email: {@email} пытается залогиниться", model.Email);
        return (await client.LoginAsync(model, GetCallOptions(cancellationToken))).Message;
    }

    public async Task<Guid> RegisterAsync(RegisterModel model, CancellationToken cancellationToken = default)
    {
        return Guid.Parse((await client.RegisterAsync(model, GetCallOptions(cancellationToken))).Value);
    }

    public async Task<UserModel> ValidateToken(Token token, CancellationToken cancellationToken = default)
    {
        var result = await client.ValidateTokenAsync(token, GetCallOptions(cancellationToken));
        return result;
    }
}