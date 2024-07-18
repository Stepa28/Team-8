namespace Domain.Models;

public record User
{
    public Guid Id { get; set; } = Guid.Empty;
    public bool IsDeleted { get; set; }
}