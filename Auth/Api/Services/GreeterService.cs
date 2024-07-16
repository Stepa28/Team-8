using Grpc.Core;
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

        public override Task<Token> Login(LoginModel request, ServerCallContext context)
        {
            return Task.FromResult(new Token{Message =request.Login + request.Password});
        }

        public override Task<UUID> Register(RegisterModel request, ServerCallContext context)
        {
            return Task.FromResult(new UUID{Value = Guid.NewGuid().ToString()});
        }
    }
}
