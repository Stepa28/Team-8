namespace Team_8.Contracts.Enums;

[Flags]
public enum PurposeOfTheMessage : ulong
{
    None
    , CreateRoom = 1 << 1
    , ConnectRoom = 1 << 2
    , DisconnectRoom = 1 << 3
    , ToggleReadiness = 1 << 4
    , StartBattle = 1 << 5
    , ChoseMap = 1 << 6
    , GetMaps = 1 << 7
    , GetRooms = 1 << 8
    , NewRoom = 1 << 9
    , DeleteRoom = 1 << 10

    , RoomAction = CreateRoom | ConnectRoom | DisconnectRoom | ToggleReadiness | StartBattle | ChoseMap | GetMaps | GetRooms
    , UpdateRoom = NewRoom | DeleteRoom
}