using Team_8.Contracts.Common;
using Team_8.Contracts.UserTransfer;

namespace Team_8.Contracts.Enums;

[Flags]
public enum PurposeOfTheMessage : uint
{
    None,

    [RequestDtoDescription(typeof(CreateRoomDto)), ResponseDtoDescription(typeof(IdDto))]
    CreateRoom = 1 << 1,

    [RequestDtoDescription(typeof(ConnectRoomDto)), ResponseDtoDescription(typeof(RoomDto))]
    ConnectRoom = 1 << 2,

    [RequestDtoDescription(typeof(IdDto)), ResponseDtoDescription(typeof(EmptyDto))]
    DisconnectRoom = 1 << 3,

    [RequestDtoDescription(typeof(IdDto)), ResponseDtoDescription(typeof(EmptyDto))]
    ToggleReadiness = 1 << 4,

    [RequestDtoDescription(typeof(IdDto)), ResponseDtoDescription(typeof(EmptyDto))]
    StartBattle = 1 << 5,

    [RequestDtoDescription(typeof(ChoseMapDto)), ResponseDtoDescription(typeof(EmptyDto))]
    ChoseMap = 1 << 6,

    [RequestDtoDescription(typeof(EmptyDto)), ResponseDtoDescription(typeof(MapsInfoDto))]
    GetMaps = 1 << 7,

    [RequestDtoDescription(typeof(EmptyDto)), ResponseDtoDescription(typeof(RoomsDto))]
    GetRooms = 1 << 8,

    [RequestDtoDescription(typeof(EmptyDto)), ResponseDtoDescription(typeof(RoomDto))]
    NewRoom = 1 << 9,

    [RequestDtoDescription(typeof(EmptyDto)), ResponseDtoDescription(typeof(IdDto))]
    DeleteRoom = 1 << 10,
    
    [RequestDtoDescription(typeof(EmptyDto)), ResponseDtoDescription(typeof(UpdateRoomDto))]
    UpdateRoom = 1 << 11,

    RoomAction = CreateRoom | ConnectRoom | DisconnectRoom | ToggleReadiness | StartBattle | ChoseMap | GetMaps | GetRooms,
    UpdateRoomMassage = NewRoom | DeleteRoom | UpdateRoom
}