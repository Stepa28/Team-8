using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Team8.Contracts.Auth.Service;

namespace Application.Mediatr.Commands.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password, string Nick) : IRequest<Guid>;

internal sealed class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger, IAuthService auth) : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var guid = await auth.RegisterAsync(new RegisterModel { Login = request.Email, Nick = request.Nick, Password = request.Password });
        return guid;
    }
}