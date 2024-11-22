using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record UserDto(Guid Id, string Nick, string Email, Role Role);