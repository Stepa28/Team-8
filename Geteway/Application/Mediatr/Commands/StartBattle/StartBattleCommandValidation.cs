using FluentValidation;

namespace Application.Mediatr.Commands.StartBattle;

public sealed class StartBattleCommandValidation : AbstractValidator<StartBattleCommand>
{
    public StartBattleCommandValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}