using MediatR;
using Team_8.Contracts.Enums;

namespace Application.Mediatr.Commands.Battle.MakeStep;

public sealed record MakeStepCommand(int BattleId, Routes Move, Routes Attack) : IRequest;