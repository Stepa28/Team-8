namespace Team_8.Contracts.UserTransfer;

public sealed record MapsInfoDto(List<MapInfoDto> Maps) : IUserTransferDto;