using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record UserDto : UserShortDto
{
    public string Email { get; set; }
    public Role Role { get; set; }
}