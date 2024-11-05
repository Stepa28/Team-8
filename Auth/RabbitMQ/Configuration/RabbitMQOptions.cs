namespace RabbitMQ.Configuration;

public class RabbitMQOptions
{
    public const string SectionKey = "MassTransit";
    public string RabbitMQHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Port { get; set; }
}