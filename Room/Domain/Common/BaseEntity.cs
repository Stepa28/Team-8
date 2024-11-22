namespace Domain.Common;

public abstract record BaseEntity
{
    public int Id { get; set; } = 0;
}