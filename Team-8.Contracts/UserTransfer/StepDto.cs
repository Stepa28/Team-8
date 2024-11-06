using Team_8.Contracts.Enums;

namespace Team_8.Contracts.UserTransfer;

public record StepDto(int BattleId, Routes Move, Routes Attack) : IUserTransferDto;