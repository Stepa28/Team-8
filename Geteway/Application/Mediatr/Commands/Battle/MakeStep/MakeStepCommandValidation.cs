using FluentValidation;

namespace Application.Mediatr.Commands.Battle.MakeStep;

public sealed class MakeStepCommandValidation : AbstractValidator<MakeStepCommand>
{
    public MakeStepCommandValidation()
    {
        RuleFor(x => x.BattleId)
            .NotEmpty()
            .GreaterThan(0);
    }
}