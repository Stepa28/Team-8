using Team8.Contracts.Auth.Service;

namespace Domain.Interfaces;

public interface IAuthService
{
    public Task<string> LoginAsync(LoginModel model, CancellationToken cancellationToken = default);
    public Task<Guid> RegisterAsync(RegisterModel model, CancellationToken cancellationToken = default);
    public Task<UserModel> ValidateToken(Token token, CancellationToken cancellationToken = default);
}