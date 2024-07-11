namespace gRPC.Configuration;

public class ServiceOptions
{
    public const string SectionKey = "gRPCAddress";
    public string Auth { get; set; }
    public string Battle { get; set; }
    public string Chat { get; set; }
    public string Room { get; set; }
}