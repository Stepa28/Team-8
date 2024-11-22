namespace Domain.Models;

public record User
{
    public Guid Id { get; set; } = Guid.Empty;
    public virtual List<UserState> UserStates { get; set; } = [];
    public bool IsDeleted { get; set; } = false;
}