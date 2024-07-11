namespace Domain.Interfaces;

public interface IAuthInfoProvider
{
    Task<string> GetAccessTokenAsync();
}