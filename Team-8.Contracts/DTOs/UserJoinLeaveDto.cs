using Team_8.Contracts.Enums;

namespace Team_8.Contracts.DTOs;

public record UserJoinLeaveDto
{
    public int RoomId { get; set; }
    public Guid UserId { get; set; }
    public string UserNick { get; set; }
    public UserJoinLeave State { get; set; }
}