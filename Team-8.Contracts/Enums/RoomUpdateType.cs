namespace Team_8.Contracts.Enums;

[Flags]
public enum RoomUpdateType
{
    None,
    Add = 1 << 1,
    Map = 1 << 2,
    Status = 1 << 3,
    CurrentRound = 1 << 4
}