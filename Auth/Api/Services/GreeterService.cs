using Grpc.Core;
using Team_8.Contracts.Enums;
using Team8.Contracts.Auth.Server;

namespace Api.Services
{
    public class GreeterService : AuthService.AuthServiceBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<UserModel> ValidateToken(Token request, ServerCallContext context)
        {
            return Task.FromResult(new UserModel
            {
                Email = "Email1Test", Id = new UUID { Value = Guid.NewGuid().ToString() }, Nick = "Nick1Test", Role = (int)Role.User
            });
        }

        public override Task<Token> Login(LoginModel request, ServerCallContext context)
        {
            return Task.FromResult(new Token{Message =request.Email + request.Password});
        }

        public override Task<UUID> Register(RegisterModel request, ServerCallContext context)
        {
            return Task.FromResult(new UUID{Value = Guid.NewGuid().ToString()});
        }
    }
}
