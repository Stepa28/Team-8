namespace Domain.Common.Configuration;

public class GrpsOptions
{
    public const string SectionKey = "gRPCOptions";
    public string Auth { get; init; }
    public string Battle { get; init; }
    public string Chat { get; init; }
    public string Room { get; init; }
    public string Shema { get; init; }
    public int DeadLineMinutes  { get; init; }

    public string BattleServer => Shema + Battle;
}