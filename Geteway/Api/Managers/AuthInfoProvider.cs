using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace Api.Managers;

public class AuthInfoProvider : IAuthInfoProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthInfoProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string> GetAccessTokenAsync()
    {
        return _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
    }
}