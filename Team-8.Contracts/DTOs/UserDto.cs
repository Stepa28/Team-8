using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record UserDto
{
    public Guid Id { get; set; }
    public string Nick { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}