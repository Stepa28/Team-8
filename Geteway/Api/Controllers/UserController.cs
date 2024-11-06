using Application.Mediatr.Commands.Auth.RegisterUser;
using Application.Mediatr.Queries.Auth.GetToken;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

public class UserController : BaseController
{
    [HttpPost("register")]
    [SwaggerOperation("Register user")]
    public Task<Guid> Register(RegisterUserCommand command) => CommandAsync<Guid>(command);
    
    [HttpPost("login")]
    [SwaggerOperation("Login user")]
    public Task<TokenModel> Login(GetTokenQuery query) => QueryAsync(query);
}