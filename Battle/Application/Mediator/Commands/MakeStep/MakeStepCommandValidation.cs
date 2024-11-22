using FluentValidation;

namespace Application.Mediator.Commands.MakeStep;

public sealed class MakeStepCommandValidation : AbstractValidator<MakeStepCommand>
{
    public MakeStepCommandValidation()
    {
        RuleFor(x => x.Model.BattleId)
            .NotEmpty();
    }
}