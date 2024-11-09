using Riok.Mapperly.Abstractions;
using Team_8.Contracts.Protos.CodeFirst;

namespace Application.Mediatr.Commands.Battle.MakeStep;

[Mapper]
public static partial class MakeStepCommandMapping
{
    public static partial StepModel MapToStepModel(this MakeStepCommand query);
}