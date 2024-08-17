using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record UserJoinLeaveDto(int RoomId, Guid UserId, string UserNick, UserJoinLeave State);