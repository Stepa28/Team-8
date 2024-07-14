using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Team8.Contracts.Auth.Service;

namespace Application.Mediatr.Queries.GetToken;

public sealed record GetTokenQuery(string Email, string Password) : IRequest<TokenModel>;

internal sealed class GetTokenQueryHandler(ILogger<GetTokenQueryHandler> logger, IAuthService auth)
    : IRequestHandler<GetTokenQuery, TokenModel>
{
    public async Task<TokenModel> Handle(GetTokenQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("1 {@email}", request.Email);
        var token = await auth.LoginAsync(new LoginModel { Login = request.Email.ToLower(), Password = request.Password });
        return new TokenModel { Token = token };
    }
}