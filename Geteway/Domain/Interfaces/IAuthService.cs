using Team8.Contracts.Auth.Service;

namespace Domain.Interfaces;

public interface IAuthService
{
    public Task<string> LoginAsync(LoginModel model);
    public Task<Guid> RegisterAsync(RegisterModel model);
}