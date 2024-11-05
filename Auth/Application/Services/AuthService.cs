using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common;
using Domain.Common.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Team_8.Contracts.DTOs;

namespace Application.Services;

public sealed class AuthService(IRepository<User> repository, ILogger<AuthService> logger) : IAuthService
{
    public async Task<string> GetToken(string email, string pass, CancellationToken cancellationToken = default)
    {
        var user = await repository.GetAsync(x => x.Email == email, cancellationToken);
        if(user == null || !PasswordHash.ValidatePassword(pass, user.PasswordHash))
            throw new NotFoundException("User not Found");

        var claims = new Claim[] { new(ClaimTypes.UserData, user.Id.ToString()) };

        return GenerateToken(claims);
    }

    public async Task<UserDto> CheckValidToken(string token, CancellationToken cancellationToken = default)
    {
        var handler = new JwtSecurityTokenHandler();

        logger.LogInformation(token);
        // Проверяем, является ли строка валидным токеном JWT
        if(!handler.CanReadToken(token))
            throw new UnauthorizedAccessException("Invalid token1");

        // Читаем токен и приводим к JwtSecurityToken
        var jwtToken = handler.ReadJwtToken(token);
        var id = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;
        if(id == null)
            throw new UnauthorizedAccessException("Invalid token2");

        var user = await repository.GetAsync(Guid.Parse(id), cancellationToken);

        if(user == null)
            throw new UnauthorizedAccessException("Invalid token3");

        return new UserDto(user.Id, user.Nickname, user.Email, user.Role);
    }

    public string GetHashPassword(string password)
    {
        return PasswordHash.HashPassword(password);
    }

    private string GenerateToken(IEnumerable<Claim>? claims = null)
    {
        var jwt = new JwtSecurityToken(
            "Auth",
            "Auth",
            claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoiZ"u8.ToArray()),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}