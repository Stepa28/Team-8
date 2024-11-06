using Riok.Mapperly.Abstractions;
using Team_8.Contracts.DTOs;
using Team8.Contracts.Auth.Service;

namespace Application.Mediatr.Commands.WebSocket.Connection;

[Mapper]
public static partial class ConnectionWebSocketCommandMapping
{
    [MapProperty(nameof(UserModel.Id), nameof(UserDto.Id), Use = nameof(MapGuid))]
    public static partial UserDto MapToUserDto(this UserModel model);

    [UserMapping(Default = false)]
    private static Guid MapGuid(UUID str) => Guid.TryParse(str.Value, out var guid) ? guid : Guid.Empty;
}