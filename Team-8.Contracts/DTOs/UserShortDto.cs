namespace Team_8.Contracts.DTOs;

public record UserShortDto
{
    public Guid Id { get; set; }
    public string Nick { get; set; }
}