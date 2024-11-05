using Domain.Common;
using Team_8.Contracts.Enums;

namespace Domain.Models;

public record User : BaseEntitySoftDelete<User>
{
    public string Nickname { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; init; }
    public Role Role { get; init; } = Role.User;
}