using Application.Common;
using Domain.Interfaces.Producers;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Grpc.Core;
using Team_8.Contracts.MassTransitDto;
using Team8.Contracts.Auth.Server;
using static System.Text.Json.JsonSerializer;

namespace Api.Controllers;

public class AuthController(IRepository<User> repository, IUserAddProducer producer, IAuthService authService, ILogger<AuthController> logger)
    : AuthService.AuthServiceBase
{
    public override async Task<UserModel> ValidateToken(Token request, ServerCallContext context)
    {
        var user = await authService.CheckValidToken(request.Message, context.CancellationToken);
            
        return new UserModel
        {
            Email = user.Email, Id = new UUID { Value = user.Id.ToString() }, Nick = user.Nick, Role = (int)user.Role
        };
    }

    public override async Task<Token> Login(LoginModel request, ServerCallContext context)
    {
        var token = await authService.GetToken(request.Email, request.Password, context.CancellationToken);
            
        return new Token { Message = token };
    }

    public override async Task<UUID> Register(RegisterModel request, ServerCallContext context)
    {
        logger.LogInformation(Serialize(request));
        var user = new User { Nickname = request.Nick, Email = request.Email, PasswordHash = PasswordHash.HashPassword(request.Password) };
        await repository.CreateAsync(user, context.CancellationToken);
        await producer.PushAddUser(new AddUserDto(user.Id, user.Nickname), context.CancellationToken);
        return new UUID { Value = user.Id.ToString() };
    }
}